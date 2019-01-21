using System;
using System.Collections.Generic;
using System.Linq;
using EasyConfig.Attributes;
using EasyConfig.Exceptions;
using EasyConfig.Internal;

namespace EasyConfig
{
    /// <summary>
    /// A section in a configuration file
    /// </summary>
    public class ConfigurationManager
    {
        private static readonly SectionMapper mapper_ = new SectionMapper();

        private static readonly Dictionary<string, Dictionary<string, ConfigurationSection>> configuration_sets_ =
            new Dictionary<string, Dictionary<string, ConfigurationSection>>();

        private static void CheckSet(string set_name)
        {
            if (!configuration_sets_.ContainsKey(set_name))
                throw new ConfigurationSetNotExistsException(set_name);
        }

        private static void CheckSection(string set_name, string section_name)
        {
            CheckSet(set_name);
            if (!configuration_sets_[set_name].ContainsKey(section_name))
                throw new SectionNotFoundException(section_name, set_name);
        }

        private static void CheckProperty(string set_name, string section_name, string property_name)
        {
            CheckSection(set_name, section_name);
            if (!configuration_sets_[set_name][section_name].Properties.ContainsKey(property_name))
                throw new PropertyNotFoundException(set_name, section_name, property_name);
        }

        private static string ResolveTypeToSectionName<T>()
        {
            if (Attribute.IsDefined(typeof(T), typeof(ConfigurationSectionAttribute), true))
            {
                var configuration_class_attribute =
                    Attribute.GetCustomAttribute(typeof(T), typeof(ConfigurationSectionAttribute), true) as
                        ConfigurationSectionAttribute;
                return string.IsNullOrEmpty(configuration_class_attribute.ClassName)
                    ? typeof(T).Name
                    : configuration_class_attribute.ClassName;
            }

            return typeof(T).Name;
        }

        public static void Load(string set_name, string configurationfile_name)
        {
            var reader = new TextConfigurationFileReader();
            var sections = reader.Read(configurationfile_name);
            configuration_sets_.Add(set_name, sections.ToDictionary(s => s.Name));
        }

        public static T GetClass<T>(string set_name)
            where T : class, new()
        {
            var resolved_section_name = ResolveTypeToSectionName<T>();

            CheckSection(set_name, resolved_section_name);

            var section = configuration_sets_[set_name][resolved_section_name];
            T target = mapper_.MapSection<T>(section /*, true*/);
            return target;
        }

        /// <summary>
        /// Can't support DefaultValueAttribute if use the method!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set_name"></param>
        /// <param name="section_name"></param>
        /// <param name="property_name"></param>
        /// <returns></returns>
        public static T GetValue<T>(string set_name, string section_name, string property_name)
        {
            CheckProperty(set_name, section_name, property_name);
            T converted;

            try
            {
                converted =
                    (T) Convert.ChangeType(configuration_sets_[set_name][section_name].Properties[property_name], typeof(T));
            }
            catch
            {
                throw new PropertyTypeMismatchException(
                    set_name,
                    section_name,
                    property_name,
                    configuration_sets_[set_name][section_name].Properties[property_name].GetType(),
                    typeof(T));
            }

            return converted;
        }
    }
}