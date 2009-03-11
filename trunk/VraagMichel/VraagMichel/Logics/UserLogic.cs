using System;
using DTO;
using Exceptions;
using RAO.Interfaces;

namespace Logics
{
    public class UserLogic
    {
        // Declarations
        private IRAOUser _IRaoUser;


        // Constructor
        public UserLogic()
        {
            //_IRaoUser = new RAOUser_EM();
        }
        public UserLogic(IRAOUser iraoUser)
        {
            if (iraoUser == null)
                throw new ArgumentNullException("iraoUser");

            _IRaoUser = iraoUser;
        }


        // Methods
        /// <summary>
        /// Deze gaat naar lokale DB op een mobiel toestel.
        /// Checkt of huidig userBarcode degene in DB.
        /// </summary>
        /// <param name="userbarcode">The userbarcode.</param>
        /// <returns></returns>
        public User GetUserLocal(string userbarcode)
        {
            if (string.IsNullOrEmpty(userbarcode))
                throw new InvalidObjectException<User>();

            var user = _IRaoUser.GetUser(userbarcode);

            return user;
        }


        /// <summary>
        /// Deze gaat naar Backend (via WS), en laadt gegevens op naar lokale DB op mobiel toestel.
        /// </summary>
        /// <param name="userbarcode">The userbarcode.</param>
        /// <returns></returns>
        public bool SynchronizeBackend(string userbarcode)
        {
            if (string.IsNullOrEmpty(userbarcode))
                throw new InvalidObjectException<User>();

            if (!_IRaoUser.SynchronizeUser(userbarcode))
                throw new ObjectNotFoundException(string.Format("Sync failer for user {0}", userbarcode));

            return true;
        }
    }
}
