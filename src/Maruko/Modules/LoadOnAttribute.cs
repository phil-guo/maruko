using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Modules
{
    /// <summary>
    /// 加载程序集的自定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LoadOnAttribute : Attribute
    {
        public string[] DependedModuleTypes { get; private set; }

        public bool IsAuto { get; private set; } = false;

        public LoadOnAttribute(bool isAuto, params string[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
            IsAuto = isAuto;
        }
    }
}
