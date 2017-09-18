using Maruko.Dependency;

namespace Maruko.Thrift.Servers
{
    public interface IServiceHandler : IDependencyTransient
    {
        /// <summary>
        ///     处理json的服务端方法
        /// </summary>
        /// <param name="jsonFormat">json 字符串</param>
        /// <returns>json字符串</returns>
        string JsonAcceptHandler(string jsonFormat);
    }
}
