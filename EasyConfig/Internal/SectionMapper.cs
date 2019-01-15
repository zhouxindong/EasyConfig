using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyConfig.Attributes;
using EasyConfig.Exceptions;

namespace EasyConfig.Internal
{
    internal class SectionMapper
    {
        private void PropertiesCheck(Type target_type, ConfigurationSection section, bool strict)
        {
            if (strict && !Attribute.IsDefined(target_type, typeof(ConfigurationSectionAttribute), true))
                throw new NoConfigurationClassException(target_type);

            Dictionary<string, PropertyInfo> target_properties
                = target_type.GetProperties().ToDictionary(GetConfigurationPropertyName);

            foreach (var section_property in section.Properties)
            {
                PropertyInfo target_property = target_properties[section_property.Key];

                if (!target_properties.ContainsKey(section_property.Key))
                    throw new TargetPropertyNotFoundException(target_type, target_property);

                if (strict && !Attribute.IsDefined(target_property, typeof(ConfigurationPropertyAttribute), true))
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

        public T MapSection<T>(ConfigurationSection section, bool strict)
            where T : class, new()
        {
            Type target_type = typeof(T);
            T target = new T();

            PropertiesCheck(target_type, section, strict);

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