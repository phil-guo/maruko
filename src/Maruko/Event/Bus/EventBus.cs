using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using log4net;
using Maruko.Event.Bus.Factories;
using Maruko.Event.Bus.Factories.Internals;
using Maruko.Event.Bus.Handlers;
using Maruko.Event.Bus.Handlers.Internals;
using Maruko.Logger;
using Maruko.Utils;

namespace Maruko.Event.Bus
{
    /// <summary>
    ///     Implements EventBus as Singleton pattern.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IEventStore _eventStore;

        /// <summary>
        ///     Gets the default <see cref="EventBus" /> instance.
        /// </summary>
        public static EventBus Default { get; } = new EventBus();

        /// <summary>
        ///     Reference to the Logger.
        /// </summary>
        public ILog Logger { get; set; }

        public EventBus()
        {
            _eventStore = ContainerManager.Current.Resolve<IEventStore>();
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(EventBus));
        }
        
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            //从事件映射集合里获取匹配当前EventData(事件源数据)的Handler
            var handlerTypes = _eventStore.GetHandlersForEvent(eventData.GetType()).ToList();
            if (handlerTypes.Count <= 0) return;
            //循环执行事件处理函数
            foreach (var handlerType in handlerTypes)
            {
                var handlerInterface = handlerType.GetInterface("IEventHandler`1");
                //这里需要根据Name才能Resolve，因为注入服务时候使用了命名方式(解决同一事件参数多个事件处理类绑定问题)+
                var eventHandler = ContainerManager.Current.ResolveNamed(handlerType.Name, handlerInterface);
                if (eventHandler.GetType().FullName == handlerType.FullName)
                {
                    var handler = eventHandler as IEventHandler<TEventData>;
                    handler?.HandleEvent(eventData);
                }
            }
        }

        public void Trigger<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
            if (_eventStore.HasRegisterForEvent<TEventData>())
            {
                var handlers = _eventStore.GetHandlersForEvent<TEventData>();
                if (handlers.Any(th => th == eventHandlerType))
                {
                    //获取类型实现的泛型接口
                    var handlerInterface = eventHandlerType.GetInterface("IEventHandler`1");
                    var eventHandlers = ContainerManager.Current.Resolve(handlerInterface);
                    //循环遍历，仅当解析的实例类型与映射字典中事件处理类型一致时，才触发事件
                    if (eventHandlers.GetType() == eventHandlerType)
                    {
                        var handler = eventHandlers as IEventHandler<TEventData>;
                        handler?.HandleEvent(eventData);
                    }
                }
            }
        }

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return Task.Run(() => Trigger(eventData));
        }
        
        public Task TriggerAsync<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
            return Task.Run(() => Trigger(eventHandlerType, eventData));
        }

        public void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData
        {
            _eventStore.RemoveRegister(typeof(TEventData), handlerType);
        }

        public void UnRegisterAll<TEventData>() where TEventData : IEventData
        {
            //获取所有映射的EventHandler
            var handlerTypes = _eventStore.GetHandlersForEvent(typeof(TEventData)).ToList();
            foreach (var handlerType in handlerTypes) _eventStore.RemoveRegister(typeof(TEventData), handlerType);
        }
    }
}