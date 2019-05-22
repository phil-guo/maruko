using System;
using System.Threading.Tasks;
using Maruko.Dependency;

namespace Maruko.Event.Bus
{
    /// <summary>
    ///     事件总线
    /// </summary>
    public interface IEventBus:IDependencySingleton
    {
        #region Unregister

        void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData;

        void UnRegisterAll<TEventData>() where TEventData : IEventData;

        #endregion

        #region Trigger

        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        void Trigger<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData;

        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        Task TriggerAsync<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData;

        #endregion
    }
}