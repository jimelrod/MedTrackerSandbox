using System;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class ResourceNotFoundException : MedicalTrackerServiceException
    {
        private const string ExceptionMessage = "{0} not found. Id: {1}. See InnerException for details...";
        
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ResourceNotFoundException(int id, Type resourceType, Exception innerException)            
            : base(GenerateExceptionMessage(id, resourceType), innerException)
        {

        }

        public ResourceNotFoundException(string id, Type resourceType, Exception innerException)
            : base(GenerateExceptionMessage(id, resourceType), innerException)
        {

        }

        private static string GenerateExceptionMessage(int id, Type resourceType)
        {
            return GenerateExceptionMessage(id.ToString(), resourceType);
        }

        private static string GenerateExceptionMessage(string id, Type resourceType)
        {
            return string.Format(ExceptionMessage, resourceType.Name.ToString(), id);
        }
    }
}