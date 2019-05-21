using Maruko.Dependency;

namespace Maruko.Events.Bus.Handlers
{
    /// <summary>
    /// 定义事件处理器公共接口，所有的事件处理都要实现该接口
    /// Implement <see cref="IEventHandler{TEventData}"/>
    /// </summary>
    public interface IEventHandler : IDependencyTransient
    {

    }
}