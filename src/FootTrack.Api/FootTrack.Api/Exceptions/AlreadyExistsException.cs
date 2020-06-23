using System;

namespace FootTrack.Api.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException() : this("Provided object already exists.")
        {

        }

        public AlreadyExistsException(string message) : base(message)
        {

        }
    }
}