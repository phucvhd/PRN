using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByEmail(string userEmail) => UserDAO.Instance.GetUserByEmail(userEmail);
        public IEnumerable<User> GetUsers() => UserDAO.Instance.GetUserList();
        public void InsertUser(User user) => UserDAO.Instance.Insert(user);
        public void DeleteUser(string userEmail) => UserDAO.Instance.Remove(userEmail);
        public void UpdateUser(User user) => UserDAO.Instance.Update(user);
    }
}
