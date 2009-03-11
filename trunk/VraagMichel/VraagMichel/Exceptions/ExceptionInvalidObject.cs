using Exceptions;

namespace Exceptions
{
    public class ExceptionInvalidObject : FB_Exception
    {
        public void SetMessage()
        {
            EntityMessage = string.Format("{0} input niet correct.", EntityName ?? "");
        }
    }

    public class InvalidObjectException : ExceptionInvalidObject
    {
        public InvalidObjectException(string type)
        {
            EntityName = type;
            SetMessage();
        }
    }

    public class InvalidObjectException<T> : ExceptionInvalidObject
    {
        public InvalidObjectException()
        {
            EntityName = typeof(T).Name;
            SetMessage();
        }
    }
}