using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByEmail(string userEmail);
        void InsertUser(User user);
        void DeleteUser(string userEmail);
        void UpdateUser(User user);
        bool CheckLogin(string email, string pass);
    }
}
