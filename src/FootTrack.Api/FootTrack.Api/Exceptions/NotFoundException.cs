using System;

namespace FootTrack.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : this("Provided object already exist.")
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }
    }
}