using System;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class ResourceNotUpdatedException : MedicalTrackerServiceException
    {
        private const string ExceptionMessage = "Unable to update resource of type: {0} with Id: {1}. See InnerException for details...";

        public ResourceNotUpdatedException()
        {
        }

        public ResourceNotUpdatedException(string message) : base(message)
        {
        }

        public ResourceNotUpdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceNotUpdatedException(int id, Type type, Exception innerException)
            : base(string.Format(ExceptionMessage, type, id), innerException)
        {

        }
    }
}