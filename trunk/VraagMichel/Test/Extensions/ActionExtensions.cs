using System;
using NUnit.Framework;

namespace Test.Extensions
{
    public static class ActionExtensions
    {
        public static T ShouldThrow<T>(this Action acton, string message) where T : Exception
        {
            try
            {
                acton();
            }
            catch (T t)
            {
                Assert.AreEqual(message, t.Message);
                return t;
            }
            throw new ArgumentException(string.Format("Expected an exception of type {0} but got none.", typeof(T)));
        }
    }
}