using System;

namespace Maruko.Event.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by ABP infrastructure.
    /// </summary>
    public class MarukoHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public MarukoHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}