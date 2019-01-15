using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class NoConfigurationClassException : Exception
    {
        public NoConfigurationClassException(Type target_type)
            : base("ConfigurationClass attribute not set for class " + target_type.Name) { }

        public NoConfigurationClassException(string message) : base(message) { }
        public NoConfigurationClassException(string message, Exception inner) : base(message, inner) { }
        protected NoConfigurationClassException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}