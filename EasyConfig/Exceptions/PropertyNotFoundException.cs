using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string set_name, string section_name, string property_name)
            : base($"Property '{set_name}' not found in section '{section_name}', set '{property_name}'") { }

        public PropertyNotFoundException(string message) : base(message) { }
        public PropertyNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected PropertyNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}