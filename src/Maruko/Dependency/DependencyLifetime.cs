namespace Maruko.Dependency
{
    /// <summary>
    /// 依赖注入类型的枚举
    /// </summary>
    public enum DependencyLifetime
    {
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 作用域
        /// </summary>
        Scoped,

        /// <summary>
        /// 瞬时
        /// </summary>
        Transient
    }
}