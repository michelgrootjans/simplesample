using System;
using DTO;
using Exceptions;
using Logics;
using NUnit.Framework;
using RAO.Interfaces;
using Rhino.Mocks;
using Test.Extensions;

namespace Test
{
    public abstract class AuthenticationServiceTest : SimpleTest<IAuthenticationService>
    {
        protected IRAOUser rao;
        protected string barcode;
        protected User user;
        protected User result;

        protected override void Arrange()
        {
            barcode = "6544685846";
            rao = MockRepository.GenerateStub<IRAOUser>();
            user = new User();
            PrepareBehaviour();
        }

        protected abstract void PrepareBehaviour();

        protected override IAuthenticationService CreateSystemUnderTest()
        {
            var authenticationService = new AuthenticationService(rao);
            return new ExceptionHanldingAuthenticationService(authenticationService);
        }
    }

    public class when_AuthenticationService_is_told_to_authenticate_a_local_user : AuthenticationServiceTest
    {
        protected override void PrepareBehaviour()
        {
            When(rao).IsToldTo(r => r.GetUser(barcode)).Return(user);
        }

        protected override void Act()
        {
            result = sut.Login(barcode);
        }

        [Test]
        public void should_tell_the_rao_to_get_the_user()
        {
            rao.AssertWasCalled(r => r.GetUser(barcode), c => c .Repeat.Once());
        }

        [Test]
        public void should_return_the_local_user()
        {
            Assert.AreSame(user, result);
        }
    }

    public class when_AuthenticationService_is_told_to_authenticate_a_remote_user : AuthenticationServiceTest
    {
        protected override void PrepareBehaviour()
        {
            When(rao).IsToldTo(r => r.GetUser(barcode)).Repeat.Once().Return(null);
            When(rao).IsToldTo(r => r.SynchronizeUser(barcode)).Return(true);
            When(rao).IsToldTo(r => r.GetUser(barcode)).Return(user);
        }

        protected override void Act()
        {
            result = sut.Login(barcode);
        }

        [Test]
        public void should_tell_the_rao_to_get_the_user_twice()
        {
            rao.AssertWasCalled(r => r.GetUser(barcode), c => c.Repeat.Twice());
        }

        [Test]
        public void should_tell_the_rao_to_synchronise_once()
        {
            rao.AssertWasCalled(r => r.SynchronizeUser(barcode));
        }

        [Test]
        public void should_return_the_local_user()
        {
            Assert.AreSame(user, result);
        }
    }

    public class when_AuthenticationService_is_told_to_authenticate_an_unknown_remote_user : AuthenticationServiceTest
    {
        private Action login;

        protected override void PrepareBehaviour()
        {
            When(rao).IsToldTo(r => r.SynchronizeUser(barcode)).Return(false);
        }

        protected override void Act()
        {
            login = () => sut.Login(barcode);
        }

        [Test]
        public void should_throw_an_axception()
        {
            login.ShouldThrow<Exception>("User not found");
        }
    }

    public class when_AuthenticationService_is_told_to_authenticate_and_rao_throws_an_exception : AuthenticationServiceTest
    {
        private Action login;
        private Exception exception;
        private string exceptionMessage;

        protected override void PrepareBehaviour()
        {
            exceptionMessage = "Hello world";
            exception = new Exception(exceptionMessage);
            When(rao).IsToldTo(r => r.SynchronizeUser(barcode)).Throw(exception);
        }

        protected override void Act()
        {
            login = () => sut.Login(barcode);
        }

        [Test]
        public void should_throw_an_exception()
        {
            var errorMsg = string.Format("Exception in Login:\r\n{0}", exceptionMessage);
            login.ShouldThrow<Exception>(errorMsg);
        }
    }

    public class when_AuthenticationService_is_told_to_authenticate_and_rao_throws_an_FB_Exception : AuthenticationServiceTest
    {
        private Action login;
        private Exception exception;
        private string exceptionMessage;

        protected override void PrepareBehaviour()
        {
            exceptionMessage = "Hello world";
            exception = new FB_Exception {EntityMessage = exceptionMessage};
            When(rao).IsToldTo(r => r.SynchronizeUser(barcode)).Throw(exception);
        }

        protected override void Act()
        {
            login = () => sut.Login(barcode);
        }

        [Test]
        public void should_throw_an_exception()
        {
            login.ShouldThrow<Exception>(exceptionMessage);
        }
    }
}
