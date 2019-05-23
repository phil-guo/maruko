using System;
using System.Runtime.Serialization;
using Maruko.Application;

namespace Maruko
{
    [Serializable]
    public class MarukoException : Exception
    {
        public ServiceEnum Status { get; set; }
        public string Msg { get; set; }

        public MarukoException()
        {
        }

        public MarukoException(string msg, ServiceEnum state = ServiceEnum.Failure)
        {
            Msg = msg;
            Status = state;
        }
       
        public MarukoException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
     
        public MarukoException(string message)
            : base(message)
        {
        }

        public MarukoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
