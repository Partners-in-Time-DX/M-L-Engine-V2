using System;

namespace ErrorHandling.Exceptions
{
    public class InvalidEnemyStateException: Exception
    {
        public InvalidEnemyStateException ()
        {}

        public InvalidEnemyStateException (string message) 
            : base(message)
        {}

        public InvalidEnemyStateException (string message, Exception innerException)
            : base (message, innerException)
        {}    
    }
}