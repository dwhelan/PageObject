using System;

namespace PageObject
{
    public class PageObjectException : Exception
    {
        public PageObjectException(string message) : base(message)
        {
        }

        public PageObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
