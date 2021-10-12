using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public interface IAgeRepository
    {
        IEnumerable<Age> GetAges();
        Age GetAgeByID(string ForAgesId);
        void InsertAge(Age Age);
        void DeleteAge(string ForAgesId);
        void UpdateAge(Age Age);
    }
}
