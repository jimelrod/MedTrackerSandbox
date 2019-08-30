using System;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class ResourceNotAddedException : MedicalTrackerServiceException
    {
        private const string ExceptionMessage = "Unable to add resource of type: {0}. See InnerException for details...";
        
        public ResourceNotAddedException()
        {
        }

        public ResourceNotAddedException(string message)
            : base(message)
        {
        }

        public ResourceNotAddedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ResourceNotAddedException(Type resourceType, Exception innerException)
            : base(string.Format(ExceptionMessage, resourceType), innerException)
        {

        }
    }
}