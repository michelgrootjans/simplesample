using System;

namespace Exceptions
{
    public class FB_Exception : Exception
    {
        public string EntityName { get; set; }
        public string EntityMessage { get; set; }
    }
}