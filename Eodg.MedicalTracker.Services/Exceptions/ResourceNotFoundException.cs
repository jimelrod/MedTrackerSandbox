using System;
using System.Runtime.Serialization;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class ResourceNotFoundException : MedicalTrackerServiceException
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}