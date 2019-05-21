using System.Threading.Tasks;

namespace Maruko.Events.Bus.Handlers
{
    /// <summary>
    /// 异步泛型事件处理器接口 <see cref="IAsyncEventHandler{TEventData}"/>.
    /// </summary>
    /// <typeparam name="TEventData">Event type to handle</typeparam>
    public interface IAsyncEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        /// Handler handles the event by implementing this method.
        /// </summary>
        /// <param name="eventData">Event data</param>
        Task HandleEventAsync(TEventData eventData);
    }
}
