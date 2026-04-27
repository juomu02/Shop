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

        public int Add(string userName, string password)
        {
            return userRepository.Add(userName, password);
        }

        public User Get(string userName)
        {
            return userRepository.Get(userName);
        }
        public bool CheckPassword(string userName, string passwordToCheck)
        {
            return userRepository.CheckPassword(userName, passwordToCheck);
        }
    }
}