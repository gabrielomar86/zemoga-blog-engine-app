using System;
using System.Runtime.Serialization;

namespace BlogEngineApp.core
{
    [Serializable]
    public class BadRequestResponseException : Exception
    {

        public BadRequestResponseException()
        {
        }

        public BadRequestResponseException(string message) : base(message)
        {
        }

        public BadRequestResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadRequestResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
