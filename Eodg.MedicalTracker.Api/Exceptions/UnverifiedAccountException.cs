using System;
using System.Runtime.Serialization;

namespace Eodg.MedicalTracker.Api.Exceptions
{
    public class UnverifiedAccountException : Exception
    {
        public UnverifiedAccountException()
        {
        }

        public UnverifiedAccountException(string message) : base(message)
        {
        }

        public UnverifiedAccountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnverifiedAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}