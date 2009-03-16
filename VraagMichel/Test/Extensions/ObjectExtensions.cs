using NUnit.Framework;

namespace Test.Extensions
{
    public static class ObjectExtensions
    {
        public static void ShouldBeEqualTo(this object actual, object expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}