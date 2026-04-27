using Shop.Entities;

namespace Shop.Repositories
{
    public interface IUserRepository
    {
        int Add(string userName, string password);
        User Get(string userName);
        bool CheckPassword(string userName, string passwordToCheck);
    }
}