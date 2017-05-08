using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Maruko.Attributes;
using Maruko.Domain.UnitOfWork;

namespace Maruko.Extensions
{
    public class AttributeExtension 
    {
        public static ContextType GetContextAttributeValue<TEntity>()
        {
            var attribute = typeof(TEntity)
                .GetTypeInfo()
                .GetCustomAttribute(typeof(ContextAttribute), false);

            if (attribute == null)
                return ContextType.DefaultContextType;

            return ((ContextAttribute)attribute).ContextType;
        }
    }
}
