using System;

namespace Maruko.Event.Bus
{
    /// <summary>
    /// Implements <see cref="IEventData"/>
    /// 事件源基类：描述事件信息，用于参数传递
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// 事件发生的时间
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected EventData()
        {
            EventTime = DateTime.Now;
        }
    }
}