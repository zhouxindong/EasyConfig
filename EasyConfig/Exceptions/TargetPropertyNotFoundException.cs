using System;
using System.Reflection;

namespace EasyConfig.Exceptions
{
    [Serializable]
    public class TargetPropertyNotFoundException : Exception
    {
        public TargetPropertyNotFoundException(Type target_type, PropertyInfo property)
            : base($"Target property '{property.Name}' not exists in class '{target_type.Name}'") { }

        public TargetPropertyNotFoundException(Type target_type, string property_name)
            : base($"Target property '{property_name}' not exists in class '{target_type.Name}'") { }

        public TargetPropertyNotFoundException(string message) : base(message) { }
        public TargetPropertyNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected TargetPropertyNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}