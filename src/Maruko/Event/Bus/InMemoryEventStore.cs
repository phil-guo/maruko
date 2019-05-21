using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maruko.Events.Bus.Handlers;

namespace Maruko.Event.Bus
{
    public class InMemoryEventStore : IEventStore
    {
        /// <summary>
        /// 定义锁对象
        /// </summary>
        private static readonly object LockObj = new object();

        private readonly ConcurrentDictionary<ValueTuple<Type, string>, Type> _eventAndHandlerMapping;

        public InMemoryEventStore()
        {
            _eventAndHandlerMapping = new ConcurrentDictionary<ValueTuple<Type, string>, Type>();
        }

        public bool IsEmpty => !_eventAndHandlerMapping.Keys.Any();

        public void AddRegister<T, TH>(string keyName) where T : IEventData where TH : IEventHandler
        {
            AddRegister(typeof(T), keyName, typeof(TH));
        }

        public void AddRegister(Type eventData, string handlerName, Type eventHandler)
        {
            lock (LockObj)
            {
                var mapperKey = new ValueTuple<Type, string>(eventData, handlerName);
                //是否存在事件参数对应的事件处理对象
                if (!HasRegisterForEvent(eventData))
                {
                    _eventAndHandlerMapping.TryAdd(mapperKey, eventHandler);
                }
                else
                {
                    _eventAndHandlerMapping[mapperKey] = eventHandler;
                }
            }
        }

        public void RemoveRegister<T, TH>() where T : IEventData where TH : IEventHandler
        {
            var handlerToRemove = FindRegisterToRemove(typeof(T), typeof(TH));
            RemoveRegister(typeof(T), handlerToRemove);
        }

        public void RemoveRegister(Type eventData, Type eventHandler)
        {
            if (eventHandler != null)
            {
                lock (LockObj)
                {
                    //移除eventHandler
                    var eventHandelerBind = _eventAndHandlerMapping.FirstOrDefault(p => p.Value == eventHandler);
                    if (eventHandelerBind.Value != null)
                    {
                        Type removedHandlers;
                        _eventAndHandlerMapping.TryRemove(eventHandelerBind.Key, out removedHandlers);
                    }
                }
            }
        }

        public bool HasRegisterForEvent<T>() where T : IEventData
        {
            var mapperDto = _eventAndHandlerMapping.FirstOrDefault(p => p.Key.Item1 == typeof(T));
            return mapperDto.Value != null ? true : false;
        }

        public bool HasRegisterForEvent(ValueTuple<Type, string> mapperKey)
        {
            return _eventAndHandlerMapping.ContainsKey(mapperKey);
        }

        public bool HasRegisterForEvent(Type eventData)
        {
            return _eventAndHandlerMapping.Count(p => p.Key.Item1 == eventData) > 0 ? true : false;
        }

        public IEnumerable<Type> GetHandlersForEvent<T>() where T : IEventData
        {
            return GetHandlersForEvent(typeof(T));
        }

        public IEnumerable<Type> GetHandlersForEvent(Type eventData)
        {
            if (HasRegisterForEvent(eventData))
            {
                var items = _eventAndHandlerMapping
                    .Where(p => p.Key.Item1 == eventData)
                    .Select(p => p.Value).ToList();
                return items;
            }
            return new List<Type>();
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventAndHandlerMapping.Keys.FirstOrDefault(eh => eh.Item2 == eventName).Item1;
        }

        public void Clear()
        {
            _eventAndHandlerMapping.Clear();
        }

        private Type FindRegisterToRemove(Type eventData, Type eventHandler)
        {
            if (!HasRegisterForEvent(eventData))
            {
                return null;
            }
            return _eventAndHandlerMapping.FirstOrDefault(p => p.Value == eventHandler).Value;
        }
    }
}
