using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyConfig.Attributes;
using EasyConfig.Exceptions;

namespace EasyConfig.Internal
{
    /// <summary>
    /// Generate a configuration class from a ConfigurationSection
    /// </summary>
    public class SectionMapper
    {
        /// <summary>
        /// ensure all properties of a section defined in the configuration file
        /// have the property definition in the related class and apply properly Attribute 
        /// </summary>
        /// <param name="target_type"></param>
        /// <param name="section"></param>
        private void PropertiesCheck(Type target_type, ConfigurationSection section/*, bool strict*/)
        {
            if (/*strict && */!Attribute.IsDefined(target_type, typeof(ConfigurationSectionAttribute), true))
                throw new NoConfigurationClassException(target_type);

            Dictionary<string, PropertyInfo> target_properties
                = target_type.GetProperties().ToDictionary(GetConfigurationPropertyName);

            // 针对配置文件的某个section中定义的所有属性
            foreach (var section_property in section.Properties)
            {
                if (!target_properties.ContainsKey(section_property.Key))
                    throw new TargetPropertyNotFoundException(target_type, section_property.Key);

                PropertyInfo target_property = target_properties[section_property.Key];

                if (/*strict && */!Attribute.IsDefined(target_property, typeof(ConfigurationPropertyAttribute), true))
                    throw new NoConfigurationPropertyException(target_type, target_property);
            }
        }

        private string GetConfigurationPropertyName(PropertyInfo property)
        {
            if (!Attribute.IsDefined(property, typeof(ConfigurationPropertyAttribute), true))
                return property.Name;

            ConfigurationPropertyAttribute configuration_field
                = Attribute.GetCustomAttribute(property, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;

            return string.IsNullOrEmpty(configuration_field.FieldName) ? property.Name : configuration_field.FieldName;
        }

        public T MapSection<T>(ConfigurationSection section/*, bool strict*/)
            where T : class, new()
        {
            Type target_type = typeof(T);
            T target = new T();

            PropertiesCheck(target_type, section/*, strict*/);

            Dictionary<string, PropertyInfo> target_properties = target_type.GetProperties().ToDictionary(GetConfigurationPropertyName);

            foreach (var section_property in section.Properties)
                target_properties[section_property.Key].SetValue(
                    target,
                    Convert.ChangeType(
                        section_property.Value, target_properties[section_property.Key].PropertyType));

            return target;
        }
    }
}