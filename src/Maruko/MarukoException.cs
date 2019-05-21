using System;
using System.Runtime.Serialization;

namespace Maruko
{
    [Serializable]
    public class MarukoException : Exception
    {
        public MarukoException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        public MarukoException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public MarukoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public MarukoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
