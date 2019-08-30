using System;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class ResourceNotDeletedException : MedicalTrackerServiceException
    {
        private const string ExceptionMessage = "Unable to delete resource of type: {0} with Id: {1}. See InnerException for details...";

        public ResourceNotDeletedException()
        {
        }

        public ResourceNotDeletedException(string message) : base(message)
        {
        }

        public ResourceNotDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceNotDeletedException(int id, Type type, Exception innerException)
            : base(string.Format(ExceptionMessage, type, id), innerException)
        {

        }
    }
}