using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EasyConfig.Exceptions;

namespace EasyConfig.Internal
{
    internal class TextConfigurationFileReader : IConfigurationFileReader
    {
        public ConfigurationSection[] Read(string file_name)
        {
            var section_declaration_regex = new Regex(@"\[(?<SectionName>[^\]]+)", RegexOptions.Compiled);
            var comments_remove_regex = new Regex(@"(//.*$)|(/\*[^(\*/)]+\*/)", RegexOptions.Compiled | RegexOptions.Multiline);
            var property_regex = new Regex("(?<PropertyName>[^=]+)=(?<PropertyValue>.+)", RegexOptions.Compiled | RegexOptions.Singleline);

            List<ConfigurationSection> sections = new List<ConfigurationSection>();
            string configurationFileContent;

            try
            {
                configurationFileContent = File.ReadAllText(file_name);
            }
            catch (Exception ex)
            {
                throw new ConfigurationFileNotFoundException(file_name, ex);
            }

            string[] lines = comments_remove_regex.Replace(configurationFileContent, string.Empty).Split('\n').Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line)).ToArray();
            ConfigurationSection section = null;

            foreach (var line in lines)
            {
                if (section_declaration_regex.Match(line).Success)
                {
                    // New section
                    section = new ConfigurationSection(section_declaration_regex.Match(line).Groups["SectionName"].Value);
                    sections.Add(section);
                }
                else
                {
                    if (section == null)
                        throw new BadConfigurationFileException(
                            string.Format("Section must be declared before property. Line = '{0}'", line));

                    Match propertyMatch = property_regex.Match(line);
                    if (propertyMatch.Success)
                        section.AddProperty(
                            propertyMatch.Groups["PropertyName"].Value.Trim(),
                            propertyMatch.Groups["PropertyValue"].Value.Trim());
                }
            }

            return sections.ToArray();
        }
    }

}