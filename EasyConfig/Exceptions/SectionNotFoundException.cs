using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class SectionNotFoundException : Exception
    {
        public SectionNotFoundException(string section_name, string set_name)
            : base($"Section '{section_name}' in configuration set '{set_name}' not found") { }
        public SectionNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected SectionNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}