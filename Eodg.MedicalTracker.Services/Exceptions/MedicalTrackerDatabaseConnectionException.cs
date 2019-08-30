using System;

namespace Eodg.MedicalTracker.Services.Exceptions
{
    public class MedicalTrackerDatabaseConnectionException : MedicalTrackerServiceException
    {
        private const string ExceptionMessage ="Unable to connect to database. See inner exception for details.";

        public MedicalTrackerDatabaseConnectionException()
            : base(ExceptionMessage)
        {

        }

        public MedicalTrackerDatabaseConnectionException(Exception ex)
            : base(ExceptionMessage, ex)
        {

        }
    }
}