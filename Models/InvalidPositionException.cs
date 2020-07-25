using System;

namespace Models
{
    public class InvalidPositionException : Exception
    {
        public InvalidPositionException(string message) : base(message) {}
    }
}