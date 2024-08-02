using System;

namespace ErrorHandling.Exceptions
{
    public class InvalidMarioStateException : Exception
    {
        public InvalidMarioStateException ()
        {}

        public InvalidMarioStateException (string message) 
            : base(message)
        {}

        public InvalidMarioStateException (string message, Exception innerException)
            : base (message, innerException)
        {}    
    }
}