using System;
using DTO;
using Exceptions;

namespace Logics
{
    //Deze klasse heb ik apart gemaakt omdat de verantwoordelijkheid enkel exception handling is
    public class ExceptionHanldingAuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationService authenticationService;

        public ExceptionHanldingAuthenticationService(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public User Login(string barcode)
        {
            try
            {
                return authenticationService.Login(barcode);
            }
            catch (FB_Exception e)
            {
                throw new Exception(e.EntityMessage);
            }
            catch(Exception e)
            {
                if (e.Message.Equals("User not found")) throw;
                throw new Exception(string.Format("Exception in Login:{0}{1}", Environment.NewLine, e.Message));
            }
        }
    }
}