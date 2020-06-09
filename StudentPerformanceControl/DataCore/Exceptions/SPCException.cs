using System;

namespace DataCore.Exceptions
{
    public class SPCException : Exception
    {
        public int StatusCode { get; }
        public SPCException(int statusCode)
        {
            StatusCode = statusCode;
        }
        public SPCException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}