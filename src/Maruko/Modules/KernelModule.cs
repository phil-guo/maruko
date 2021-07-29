using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Core.Modules
{
    public abstract class KernelModule : Module
    {
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            try
            {
                base.Load(builder);
                RegisterModule(builder);
            }
            catch (Exception e)
            {
                throw new Exception($"模块注册发生错误：{e.Message}");
            }
        }

        public virtual void Initialize(ILifetimeScope scope)
        {

        }

        /// <summary>
        /// 模块注册基础方法
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void RegisterModule(ContainerBuilder builder)
        {
        }
    }
}
