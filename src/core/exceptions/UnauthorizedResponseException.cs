using System;
using System.Runtime.Serialization;

namespace BlogEngineApp.core
{
    [Serializable]
    public class UnauthorizedResponseException : Exception
    {

        public UnauthorizedResponseException()
        {
        }

        public UnauthorizedResponseException(string message) : base(message)
        {
        }

        public UnauthorizedResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
