using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public abstract class SimpleTest<SystemUnderTest>
    {
        protected SystemUnderTest sut;

        [SetUp]
        public void SetUp()
        {
            Arrange();
            sut = CreateSystemUnderTest();
            Act();
        }

        protected abstract void Arrange();
        protected abstract SystemUnderTest CreateSystemUnderTest();
        protected abstract void Act();

        protected IStubSpecification<Target> When<Target>(Target t) where Target : class
        {
            return new StubSpecification<Target>(t);
        }
    }
}