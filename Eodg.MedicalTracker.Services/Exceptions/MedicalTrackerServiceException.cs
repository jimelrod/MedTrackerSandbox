using System;
using System.Runtime.Serialization;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class MedicalTrackerServiceException : Exception
    {
        public MedicalTrackerServiceException()
        {
        }

        public MedicalTrackerServiceException(string message) : base(message)
        {
        }

        public MedicalTrackerServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedicalTrackerServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}