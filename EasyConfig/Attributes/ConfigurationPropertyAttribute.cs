using System;

namespace EasyConfig.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ConfigurationPropertyAttribute : Attribute
    {
        public string FieldName { get; set; }

        public ConfigurationPropertyAttribute()
        {
            FieldName = string.Empty;
        }

        public ConfigurationPropertyAttribute(string field_name)
        {
            FieldName = field_name;
        }
    }
}