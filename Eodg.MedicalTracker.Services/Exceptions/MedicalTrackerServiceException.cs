using System;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public abstract class MedicalTrackerServiceException : Exception
    {
        public MedicalTrackerServiceException()
        {
        }

        public MedicalTrackerServiceException(string message)
            : base(message)
        {
        }

        public MedicalTrackerServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}