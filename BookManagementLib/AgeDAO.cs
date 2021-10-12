using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib
{
    class AgeDAO
    {
        private static AgeDAO instance = null;
        private static readonly object instanceLock = new object();
        public static AgeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AgeDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Age> GetAgeList()
        {
            var Ages = new List<Age>();
            try
            {
                using var context = new BookManagementDBContext();
                Ages = context.Ages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ages;
        }

        public Age GetAgeByID(string ForAgesId)
        {
            Age Age = null;
            try
            {
                using var context = new BookManagementDBContext();
                Age = context.Ages.SingleOrDefault(m => m.ForAgesId == ForAgesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Age;
        }


        public void Insert(Age Age)
        {
            try
            {
                Age age = GetAgeByID(Age.ForAgesId);
                if (age == null)
                {
                    using var context = new BookManagementDBContext();
                    context.Ages.Add(Age);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Age is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Age Age)
        {
            try
            {
                Age age = GetAgeByID(Age.ForAgesId);
                if (age != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Ages.Update(Age);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Age does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string ForAgesId)
        {
            try
            {
                Age Age = GetAgeByID(ForAgesId);
                if (Age != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Ages.Remove(Age);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Age does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string IdGenerate()
        {
            string newid=null;
            try
            {
                using var context = new BookManagementDBContext();
                Age age = context.Ages.ToList().OrderByDescending(a=>a.ForAgesId).FirstOrDefault();
                if (age == null)
                {
                    newid = "A1";
                }
                else
                {
                    newid = age.ForAgesId.Substring(0,1) + (Int32.Parse(age.ForAgesId.Substring(1)) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newid;
        }
    }
}
