using System;

namespace ErrorHandling.Exceptions
{
    public class InvalidPlayerActionException : Exception
    {
        public InvalidPlayerActionException() {}
        
        public InvalidPlayerActionException (string message) 
            : base(message)
        {}

        public InvalidPlayerActionException (string message, Exception innerException)
            : base (message, innerException)
        {}    
    }
}