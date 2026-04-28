using Shop.Entities;

namespace Shop.Repositories
{
    public interface IUserRepository
    {
        int? Add(string userName, string password);
        User Get(string userName);
        User Get(int userId);
        bool CheckPassword(int userId, string passwordToCheck);
        void ChangePassword(int userId, string newPassword);
        void Remove(int userId);
    }
}