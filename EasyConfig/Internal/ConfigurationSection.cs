using System;
using System.Collections.Generic;
using System.Text;
using EasyConfig.Exceptions;

namespace EasyConfig.Internal
{
    /// <summary>
    /// [XXXX]
    /// </summary>
    public class ConfigurationSection
    {
        /// <summary>
        /// Gets section name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets properties of section
        /// </summary>
        public Dictionary<string, object> Properties { get; }

        public ConfigurationSection(string section_name)
        {
            Name = section_name;
            Properties = new Dictionary<string, object>();
        }

        public void AddProperty(string name, object value)
        {
            if (Properties.ContainsKey(name))
                throw new DuplicatePropertyNameException(name, Name);

            Properties.Add(name, value);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"Section \"{Name}\"{Environment.NewLine}");

            foreach (var property in Properties)
                sb.Append($"   {property.Key} = {property.Value}{Environment.NewLine}");

            return sb.ToString();
        }
    }
}