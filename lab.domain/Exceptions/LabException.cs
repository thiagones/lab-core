using System;

namespace lab.domain.Exceptions
{
    public class LabException : ApplicationException
    {
        public int ErrorCode { get; }

        public LabException()
        {
            ErrorCode = 0;
        }

        public LabException(string message)
            : base(message)
        {
            ErrorCode = 0;
        }

        public LabException(string message, Exception exception)
            : base(message, exception)
        {
            ErrorCode = 0;
        }

        public LabException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public LabException(string message, int errorCode, ApplicationException inner)
            : base(message, inner)
        {
            ErrorCode = errorCode;
        }
    }
}