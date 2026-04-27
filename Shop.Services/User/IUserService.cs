using Shop.Entities;

namespace Shop.Repositories
{
    public interface IUserService
    {
        int Add(string userName, string password);
        User Get(string userName);
        bool CheckPassword(string userName, string passwordToCheck);
    }
}