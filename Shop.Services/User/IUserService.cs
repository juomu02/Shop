using Shop.Entities;

namespace Shop.Repositories
{
    public interface IUserService
    {
        int? Add(string userName, string password);
        User Get(string userName);
        bool CheckPassword(int userId, string passwordToCheck);
        void ChangePassword(int userId, string newPassword);
        void Remove(int userId);

    }
}