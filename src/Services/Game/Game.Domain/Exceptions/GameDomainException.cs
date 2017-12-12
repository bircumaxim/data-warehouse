using System;

namespace Game.Domain.Exceptions
{
    public class GameDomainException : Exception
    {
        public GameDomainException()
        {
        }

        public GameDomainException(string message) : base(message)
        {
        }

        public GameDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}