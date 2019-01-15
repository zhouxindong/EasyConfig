using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class BadConfigurationFileException : Exception
    {
        public BadConfigurationFileException() { }
        public BadConfigurationFileException(string message) : base(message) { }
        public BadConfigurationFileException(string message, Exception inner) : base(message, inner) { }
        protected BadConfigurationFileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}