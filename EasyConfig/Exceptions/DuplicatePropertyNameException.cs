using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class DuplicatePropertyNameException : Exception
    {
        public DuplicatePropertyNameException(string property_name, string section_name)
            : base($"Duplicate property '{property_name}' in section '{section_name}") { }
        public DuplicatePropertyNameException(string message) : base(message) { }
        public DuplicatePropertyNameException(string message, Exception inner) : base(message, inner) { }
        protected DuplicatePropertyNameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}