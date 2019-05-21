using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Autofac;
using Maruko.Dependency.Installers;
using Maruko.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Module = Autofac.Module;

namespace Maruko.Modules
{
    public abstract class MarukoModule : Module
    {
        /// <summary>
        /// 设置模块顺序
        /// </summary>
        public virtual double Order { get; set; }

        public virtual bool IsLastModule { get; set; } = false;

        protected override Assembly ThisAssembly
        {
            get
            {
                Type type = this.GetType();
                Type baseType = type.GetTypeInfo().BaseType;
                if ((object)baseType != (object)typeof(MarukoModule))
                    throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture,
                        "ThisAssembly仅在直接从MarukoModule继承的模块中可用,它不能用于{0},它继承自{1}", new object[2]
                        {
                            (object) type,
                            (object) baseType
                        }));
                return type.GetTypeInfo().Assembly;
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            TypeFindExtensions.AddAssmbly(ThisAssembly);

            if (IsLastModule)
                builder.AddDependencyRegister(TypeFindExtensions.DictionaryAssembly["Assembly"] ?? new List<Assembly>());

            base.Load(builder);
        }
    }
}
    