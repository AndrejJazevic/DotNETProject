using System;

namespace Application.Exceptions
{
    public class UserDoesNotHaveAccessException : Exception
    {
        public UserDoesNotHaveAccessException()
            : base()
        {
        }

        public UserDoesNotHaveAccessException(string message)
            : base(message)
        {
        }
    }
}