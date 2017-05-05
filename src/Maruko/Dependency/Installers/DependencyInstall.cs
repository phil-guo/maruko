using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Maruko.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Dependency.Installers
{
    /// <summary>
    ///     自动进行依赖注入
    /// </summary>
    public static class DependencyInstall
    {
        /// <summary>
        ///     对三种生命周期的DI进行依赖注入注册
        /// </summary>
        public static void AddDependencyRegister(this IServiceCollection service)
        {
            service.AddServiceByLifetime(DependencyLifetime.Singleton);
            service.AddServiceByLifetime(DependencyLifetime.Scoped);
            service.AddServiceByLifetime(DependencyLifetime.Transient);
        }

        /// <summary>
        /// </summary>
        /// <param name="services">服务的注册与提供</param>
        /// <param name="lifetime">生命周期</param>
        private static void AddServiceByLifetime(this IServiceCollection services, DependencyLifetime lifetime)
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

            var assemblies = ReflectionHelper.GetAssemblies();

            var definedTypes = new List<TypeInfo>();

            foreach (var assembly in assemblies)
            {
                var definedType = assembly.DefinedTypes.ToList();

                foreach (var typeInfo in definedType)
                    definedTypes.Add(typeInfo);
            }

            var types = definedTypes.Where(typeInfo => baseType.IsAssignableFrom(typeInfo.AsType()));
            var interfaceTypeInfos = types.Where(t => t.IsInterface && t.AsType() != baseType);
            var implTypeInfos = types.Where(t => t.IsClass && !t.IsAbstract);

            foreach (var interfaceTypeInfo in interfaceTypeInfos)
            {
                var interfaceType = interfaceTypeInfo.AsType();
                Type implType = null;
                if (!interfaceTypeInfo.IsGenericType)
                    implType = implTypeInfos.FirstOrDefault(t => interfaceTypeInfo.IsAssignableFrom(t))?.AsType();
                else
                    implType = implTypeInfos.FirstOrDefault(t =>
                            t.ImplementedInterfaces.Any(x =>
                            {
                                var typeInfo = x.GetTypeInfo();
                                return typeInfo.Namespace == interfaceTypeInfo.Namespace
                                       && typeInfo.Name == interfaceTypeInfo.Name;
                            }))
                        ?.AsType();

                if (implType == null)
                    continue;

                switch (lifetime)
                {
                    case DependencyLifetime.Singleton:
                        services.AddSingleton(interfaceType, implType);
                        break;
                    case DependencyLifetime.Scoped:
                        services.AddScoped(interfaceType, implType);
                        break;
                    case DependencyLifetime.Transient:
                        services.AddTransient(interfaceType, implType);
                        break;
                    default:
                        throw new ArgumentException("Error Lifetime");
                }
            }
        }
    }
}