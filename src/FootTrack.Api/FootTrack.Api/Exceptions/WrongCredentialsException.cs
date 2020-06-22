using System;

namespace FootTrack.Api.Exceptions
{
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException() : this("Provided wrong credentials.")
        {

        }

        public WrongCredentialsException(string message) : base(message)
        {

        }
    }
}
