namespace Exceptions
{
    public class ExceptionObjectNotFound : FB_Exception
    {
        public void SetMessage()
        {
            EntityMessage = string.Format("{0} niet gevonden.", EntityName ?? "");
        }
    }

    public class ObjectNotFoundException : ExceptionObjectNotFound
    {
        public ObjectNotFoundException(string type)
        {
            EntityName = type;
            SetMessage();
        }
    }

    public class ObjectNotFoundException<T> : ExceptionObjectNotFound
    {
        public ObjectNotFoundException()
        {
            EntityName = typeof(T).Name;
            SetMessage();
        }
    }
}