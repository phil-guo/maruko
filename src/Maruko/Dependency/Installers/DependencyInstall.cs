using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Maruko.Logger;
using Maruko.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Maruko.Dependency.Installers
{
    /// <summary>
    ///     自动进行依赖注入
    /// </summary>
    public static class DependencyInstall
    {
        private static ILog Logger { get; set; }

        static DependencyInstall()
        {
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(DependencyInstall));
        }

        public static void AddDependencyRegister(this IServiceCollection service, IEnumerable<Assembly> assemblies)
        {
            service.AddServiceByLifetime(DependencyLifetime.Singleton, assemblies);
            service.AddServiceByLifetime(DependencyLifetime.Scoped, assemblies);
            service.AddServiceByLifetime(DependencyLifetime.Transient, assemblies);
        }

        /// <summary>
        ///     对三种生命周期的DI进行依赖注入注册
        /// </summary>
        public static void AddDependencyRegister(this IServiceCollection service)
        {
            var assemblies = ReflectionHelper.GetAssembliyList();//ReflectionHelper.Assemblies ?? new List<Assembly>();

            //Logger.Debug($"asscemy count : {assemblies.Count()}");

            service.AddServiceByLifetime(DependencyLifetime.Singleton, assemblies);
            service.AddServiceByLifetime(DependencyLifetime.Scoped, assemblies);
            service.AddServiceByLifetime(DependencyLifetime.Transient, assemblies);
        }

        /// <summary>
        ///  依赖注入
        /// </summary>
        /// <param name="services">服务的注册与提供</param>
        /// <param name="lifetime">生命周期</param>
        /// <param name="assemblies"></param>
        private static void AddServiceByLifetime(this IServiceCollection services,
            DependencyLifetime lifetime,
            IEnumerable<Assembly> assemblies)
        {
            var baseType = lifetime == DependencyLifetime.Singleton
                ? typeof(IDependencySingleton)
                : lifetime == DependencyLifetime.Scoped
                    ? typeof(IDependencyScoped)
                    : lifetime == DependencyLifetime.Transient
                        ? typeof(IDependencyTransient)
                        : null;

            if (baseType == null)
                throw new ArgumentException("lifetime error");

            var definedTypes = assemblies.SelectMany(assembly => assembly.DefinedTypes.ToList()).ToList();

            var types = definedTypes.Where(typeInfo => baseType.IsAssignableFrom(typeInfo.AsType()));
            var interfaceTypeInfos = types.Where(t => t.IsInterface && t.AsType() != baseType);
            var implTypeInfos = types.Where(t => t.IsClass && !t.IsAbstract);

            //改造为一个接口对应多个类型的依赖注入逻辑
            foreach (var interfaceTypeInfo in interfaceTypeInfos)
            {
                var interfaceType = interfaceTypeInfo.AsType();
                List<Type> implTypes = null;//Type implType = null;
                if (!interfaceTypeInfo.IsGenericType)
                    implTypes = implTypeInfos?.Where(t => interfaceTypeInfo.IsAssignableFrom(t)).Select(it => it.AsType()).ToList();
                else
                    implTypes = implTypeInfos?.Where(t =>
                            t.ImplementedInterfaces.Any(x =>
                            {
                                var typeInfo = x.GetTypeInfo();
                                return typeInfo.Namespace == interfaceTypeInfo.Namespace
                                       && typeInfo.Name == interfaceTypeInfo.Name;
                            }))
                        .Select(it => it.AsType()).ToList();

                if ((implTypes?.Count ?? 0) <= 0)
                    continue;

                switch (lifetime)
                {
                    case DependencyLifetime.Singleton:
                        implTypes.ForEach(it => { services.TryAddSingleton(interfaceType, it); });
                        //services.AddSingleton(interfaceType, implTypes);
                        break;
                    case DependencyLifetime.Scoped:
                        implTypes.ForEach(it => { services.TryAddScoped(interfaceType, it); });
                        //services.AddScoped(interfaceType, implTypes);
                        break;
                    case DependencyLifetime.Transient:
                        implTypes.ForEach(it => { services.AddTransient(interfaceType, it); });
                        //services.AddTransient(interfaceType, implTypes);
                        break;
                    default:
                        throw new ArgumentException("Error Lifetime");
                }
            }
        }
    }
}