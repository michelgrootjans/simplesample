using System;
using DTO;
using RAO.Interfaces;

namespace Logics
{
    //Hier is het niet nodig deze klasse nog op te splitsen, het is kort en overzichtelijk
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRAOUser rao;

        public AuthenticationService(IRAOUser rao)
        {
            this.rao = rao;
        }

        public User Login(string barcode)
        {
            return rao.GetUser(barcode) ?? SynchronizeAndGetUser(barcode);
        }

        private User SynchronizeAndGetUser(string barcode)
        {
            if(!rao.SynchronizeUser(barcode))
                throw new Exception("User not found");

            return rao.GetUser(barcode);
        }
    }
}