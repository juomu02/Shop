using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class UserService : IUserService
    {
        public IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public int? Add(string userName, string password)
        {
            return userRepository.Add(userName, password);
        }

        public User Get(string userName)
        {
            return userRepository.Get(userName);
        } public User Get(int userId)
        {
            return userRepository.Get(userId);
        }
        public bool CheckPassword(int userId, string passwordToCheck)
        {
            return userRepository.CheckPassword(userId, passwordToCheck);
        }
        public void ChangePassword(int userId, string newPassword)
        {
            userRepository.ChangePassword(userId, newPassword);
        }
        public void Remove(int userId)
        {
            userRepository.Remove(userId);
        }
    }
}