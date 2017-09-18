using System;
using Maruko.Domain.UnitOfWork;

namespace Maruko.Attributes
{
    /// <summary>
    /// ef数据库上下文特性
    /// </summary>
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
