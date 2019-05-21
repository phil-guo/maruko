using System;

namespace Maruko.Event.Bus
{
    /// <summary>
    /// 定义事件源接口，所有的事件源都要实现该接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件发生的时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        object EventSource { get; set; }
    }
}
