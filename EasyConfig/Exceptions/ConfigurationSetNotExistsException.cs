using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class ConfigurationSetNotExistsException : Exception
    {
        public ConfigurationSetNotExistsException(string set_name)
            : base($"Configuration set '{set_name}' not found") { }
        public ConfigurationSetNotExistsException(string message, Exception inner) : base(message, inner) { }
        protected ConfigurationSetNotExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}