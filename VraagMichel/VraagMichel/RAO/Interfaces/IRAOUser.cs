using DTO;

namespace RAO.Interfaces
{
    public interface IRAOUser
    {
        User GetUser(string userCode);
        bool SynchronizeUser(string userbarcode);
    }
}