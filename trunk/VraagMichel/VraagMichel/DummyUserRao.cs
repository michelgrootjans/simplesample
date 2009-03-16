using DTO;
using RAO.Interfaces;

namespace VraagMichel
{
    internal class DummyUserRao : IRAOUser
    {
        public User GetUser(string userCode)
        {
            return new User{Name = "Michel"};
        }

        public bool SynchronizeUser(string userbarcode)
        {
            return true;
        }
    }
}