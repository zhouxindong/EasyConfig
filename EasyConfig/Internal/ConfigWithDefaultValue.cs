using System;
using System.ComponentModel;
using EasyConfig.Attributes;

namespace EasyConfig.Internal
{
    public abstract class ConfigWithDefaultValue
    {
        protected ConfigWithDefaultValue()
        {
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                if (!Attribute.IsDefined(property, typeof(DefaultValueAttribute), true)) continue;
                var default_value =
                    Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                if (default_value == null) continue;
                property.SetValue(this, Convert.ChangeType(default_value.Value, property.PropertyType));
            }
        }
    }
}