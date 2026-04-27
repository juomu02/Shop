using Shop.Data;
using Shop.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopDbContext dbContext;

        public UserRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(string userName, string password)
        {

            var user = CreateUser(userName, password);
            var entityEntry = dbContext.Users.Add(user);

            dbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }
        public User Get(string userName)
        {
            return dbContext.Users.SingleOrDefault(o => o.UserName == userName);
        }
        public bool CheckPassword(string userName, string passwordToCheck)
        {
            var user = Get(userName);
            var enteredPwd = EncryptString(passwordToCheck);
            for (int index = 0; index < enteredPwd.Length; index++)
            {
                if (enteredPwd[index] != user.Password[index])
                    return false;
            }
            return true;
        }
        private User CreateUser(string userName, string password)
        {
            return new User
            {
                UserName = userName,
                Password = EncryptString(password)
            };
        }
        private byte[] EncryptString(string passwordString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(passwordString));

            return hashValue;
        }
    }
}