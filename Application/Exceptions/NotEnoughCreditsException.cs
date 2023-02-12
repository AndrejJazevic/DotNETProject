using System;

namespace Application.Exceptions
{
    public class NotEnoughCreditsException : Exception
    {
        public NotEnoughCreditsException()
            : base()
        {
        }

        public NotEnoughCreditsException(string message)
            : base(message)
        {
        }
    }
}