using System;
using System.Runtime.Serialization;

namespace Pizza.Services
{
    [Serializable]
    internal class DataBasenException : Exception
    {
        public DataBasenException()
        {
        }

        public DataBasenException(string message) : base(message)
        {
        }

        public DataBasenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataBasenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}