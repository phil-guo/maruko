using System;
using System.Collections.Generic;
using Maruko.Event.Bus.Handlers;

namespace Maruko.Event.Bus
{
    /// <summary>
    ///     定义事件源与事件处理对象存储容器接口
    /// </summary>
    public interface IEventStore
    {
        bool IsEmpty { get; }

        void AddRegister<T, TH>(string keyName) where T : IEventData
            where TH : IEventHandler;

        void AddRegister(Type eventData, string handlerName, Type eventHandler);


        void RemoveRegister<T, TH>() where T : IEventData
            where TH : IEventHandler;

        void RemoveRegister(Type eventData, Type eventHandler);

        bool HasRegisterForEvent<T>()
            where T : IEventData;

        bool HasRegisterForEvent(Type eventData);

        IEnumerable<Type> GetHandlersForEvent<T>()
            where T : IEventData;

        IEnumerable<Type> GetHandlersForEvent(Type eventData);

        Type GetEventTypeByName(string eventName);

        void Clear();
    }
}