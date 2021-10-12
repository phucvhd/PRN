using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByEmail(string userEmail);
        void InsertUser(User user);
        void DeleteUser(string userEmail);
        void UpdateUser(User user);
    }
}
