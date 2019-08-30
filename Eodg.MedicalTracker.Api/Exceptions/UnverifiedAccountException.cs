using System;

namespace Eodg.MedicalTracker.Api.Exceptions
{
    public class UnverifiedAccountException : Exception
    {
        private const string ExceptionMessage = "Email account not verified";

        public UnverifiedAccountException()
            : base(ExceptionMessage)
        {
        }

        public UnverifiedAccountException(string message)
            : base(message)
        {
        }

        public UnverifiedAccountException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnverifiedAccountException(Exception innerException)
            : base(ExceptionMessage, innerException)
        {
        }
    }
}