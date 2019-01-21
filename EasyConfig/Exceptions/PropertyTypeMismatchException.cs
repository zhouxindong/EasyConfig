using System;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class PropertyTypeMismatchException : Exception
    {
        public PropertyTypeMismatchException(string set_name, string section_name, string property_name,
            Type source_type, Type target_type)
            : base(
                $"Property type mismatch, can't convert '{source_type.Name}' to '{target_type.Name}' [" +
                $"Set = '{set_name}', Section = '{section_name}', Property = '{property_name}']")
        {
        }

        public PropertyTypeMismatchException(string message) : base(message)
        {
        }

        public PropertyTypeMismatchException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PropertyTypeMismatchException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }

}