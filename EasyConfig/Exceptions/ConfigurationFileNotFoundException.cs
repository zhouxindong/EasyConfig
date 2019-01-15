using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class ConfigurationFileNotFoundException : Exception
    {
        public ConfigurationFileNotFoundException(string file_name) : this(file_name, null) { }
        public ConfigurationFileNotFoundException(string file_name, Exception inner)
            : base($"Configuration file '{file_name}' not found", inner) { }
        protected ConfigurationFileNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}