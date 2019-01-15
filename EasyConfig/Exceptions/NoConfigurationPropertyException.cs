using System;
using System.Reflection;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class NoConfigurationPropertyException : Exception
    {
        public NoConfigurationPropertyException(Type target_type, PropertyInfo property)
            : base(
                $"ConfigurationProperty attribute not set for property '{property.Name}' in class '{target_type.Name}'") { }
        public NoConfigurationPropertyException(string message) : base(message) { }
        public NoConfigurationPropertyException(string message, Exception inner) : base(message, inner) { }
        protected NoConfigurationPropertyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}