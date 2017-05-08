using System;
using Maruko.Domain.UnitOfWork;

namespace Maruko.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = true)]
    public class ContextAttribute : Attribute
    {
        public ContextType ContextType;

        public ContextAttribute(ContextType contextType)
        {
            ContextType = contextType;
        }
    }
}
