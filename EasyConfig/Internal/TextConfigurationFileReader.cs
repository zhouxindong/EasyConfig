using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasyConfig.Exceptions;
using SharpKnife.Text;

namespace EasyConfig.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class TextConfigurationFileReader : IConfigurationFileReader
    {
        //public ConfigurationSection[] Read(string file_name)
        //{
        //    var section_declaration_regex = new Regex(@"\[(?<SectionName>[^\]]+)", RegexOptions.Compiled);
        //    var comments_remove_regex = new Regex(@"(//.*$)|(/\*[^(\*/)]+\*/)",
        //        RegexOptions.Compiled | RegexOptions.Multiline);
        //    var property_regex = new Regex("(?<PropertyName>[^=]+)=(?<PropertyValue>.+)",
        //        RegexOptions.Compiled | RegexOptions.Singleline);

        //    List<ConfigurationSection> sections = new List<ConfigurationSection>();
        //    string configurationFileContent;

        //    try
        //    {
        //        configurationFileContent = File.ReadAllText(file_name);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConfigurationFileNotFoundException(file_name, ex);
        //    }

        //    string[] lines =
        //        comments_remove_regex.Replace(configurationFileContent, string.Empty)
        //            .Split('\n')
        //            .Select(line => line.Trim())
        //            .Where(line => !string.IsNullOrEmpty(line))
        //            .ToArray();
        //    ConfigurationSection section = null;

        //    foreach (var line in lines)
        //    {
        //        if (section_declaration_regex.Match(line).Success)
        //        {
        //            // New section
        //            section = new ConfigurationSection(section_declaration_regex.Match(line).Groups["SectionName"].Value);
        //            sections.Add(section);
        //        }
        //        else
        //        {
        //            if (section == null)
        //                throw new BadConfigurationFileException(
        //                    string.Format("Section must be declared before property. Line = '{0}'", line));

        //            Match propertyMatch = property_regex.Match(line);
        //            if (propertyMatch.Success)
        //                section.AddProperty(
        //                    propertyMatch.Groups["PropertyName"].Value.Trim(),
        //                    propertyMatch.Groups["PropertyValue"].Value.Trim());
        //        }
        //    }

        //    return sections.ToArray();
        //}

        public Dictionary<string, List<Tuple<string, string>>> ReadConfigFile(string file_name)
        {
            if (!File.Exists(file_name))
                throw new ConfigurationFileNotFoundException(file_name);

            var config_txt_all = File.ReadAllText(file_name);
            var config_txt_nocomment = RegEx.CommonCommentRegex.Replace(config_txt_all, string.Empty).Split('\n')
                .Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line)).ToArray();

            string processing_section = null;
            var ret = new Dictionary<string, List<Tuple<string, string>>>();

            foreach (var line in config_txt_nocomment)
            {
                if (RegEx.IniSectionRegex.Match(line).Success)
                {
                    processing_section = RegEx.IniSectionRegex.Match(line).Groups["SectionName"].Value;
                    ret.Add(processing_section, new List<Tuple<string, string>>());
                }
                else
                {
                    if (string.IsNullOrEmpty(processing_section))
                        continue;
                    var property_name_match = RegEx.IniPropertyNameRegex.Match(line);
                    if (property_name_match.Success &&
                        !string.IsNullOrEmpty(property_name_match.Groups["PropertyName"].Value))
                    {
                        ret[processing_section].Add(new Tuple<string, string>(
                            property_name_match.Groups["PropertyName"].Value,
                            RegEx.IniPropertyValueRegex.Match(line).Groups["PropertyValue"].Value.Trim()));
                    }
                }
            }
            return ret;
        }

        public List<ConfigurationSection> Read(string file_name)
        {
            var sections = new List<ConfigurationSection>();
            var sections_txt = ReadConfigFile(file_name);
            foreach (var section_txt in sections_txt)
            {
                var section = new ConfigurationSection(section_txt.Key);
                foreach (var properties_txt in section_txt.Value)
                {
                    section.AddProperty(properties_txt.Item1, properties_txt.Item2);
                }
                sections.Add(section);
            }

            return sections;
        }
    }
}