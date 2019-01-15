using System;

namespace EasyConfig.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ConfigurationSectionAttribute : Attribute
    {
        public string ClassName { get; set; }

        public ConfigurationSectionAttribute()
        {
            ClassName = string.Empty;
        }

        public ConfigurationSectionAttribute(string class_name)
        {
            ClassName = class_name;
        }
    }
}