using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Test
{
    public interface IStubSpecification<Target> where Target : class
    {
        IMethodOptions<Result> IsToldTo<Result>(Function<Target, Result> func);
    }

    internal class StubSpecification<Target> : IStubSpecification<Target> where Target : class
    {
        private readonly Target target;

        public StubSpecification(Target t)
        {
            target = t;
        }

        public IMethodOptions<Result> IsToldTo<Result>(Function<Target, Result> func)
        {
            return target.Stub(func);
        }
    }
}