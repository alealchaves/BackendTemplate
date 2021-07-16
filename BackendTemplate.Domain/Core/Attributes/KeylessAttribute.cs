using System;

namespace BackendTemplate.Domain.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class KeylessAttribute : System.Attribute
    {
        public KeylessAttribute()
        {

        }
    }
}
