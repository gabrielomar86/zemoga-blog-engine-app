using System;
using System.Runtime.Serialization;

namespace BlogEngineApp.core
{
    [Serializable]
    public class NotFoundResponseException : Exception
    {

        public NotFoundResponseException()
        {
        }

        public NotFoundResponseException(string message) : base(message)
        {
        }

        public NotFoundResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
