using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public class AgeRepository : IAgeRepository
    {
        public Age GetAgeByID(string ForAgesId) => AgeDAO.Instance.GetAgeByID(ForAgesId);
        public IEnumerable<Age> GetAges() => AgeDAO.Instance.GetAgeList();
        public void InsertAge(Age age) => AgeDAO.Instance.Insert(age);
        public void DeleteAge(string ForAgesId) => AgeDAO.Instance.Remove(ForAgesId);
        public void UpdateAge(Age age) => AgeDAO.Instance.Update(age);
        public string AgeIdGenerate() => AgeDAO.Instance.IdGenerate();
    }
}
