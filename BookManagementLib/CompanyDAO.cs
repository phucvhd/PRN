using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;

namespace BookManagementLib
{
    class CompanyDAO
    {
        private static CompanyDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CompanyDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CompanyDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Company> GetCompanyList()
        {
            var Companys = new List<Company>();
            try
            {
                using var context = new BookManagementDBContext();
                Companys = context.Companies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Companys;
        }

        public Company GetCompanyByID(string CompanyID)
        {
            Company Company = null;
            try
            {
                using var context = new BookManagementDBContext();
                Company = context.Companies.SingleOrDefault(m => m.CompanyId == CompanyID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Company;
        }

        public Company GetCompanyByName(string CompanyName)
        {
            Company Company = null;
            try
            {
                using var context = new BookManagementDBContext();
                Company = context.Companies.SingleOrDefault(m => m.CompanyName == CompanyName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Company;
        }

        public void Insert(Company Company)
        {
            try
            {
                Company sup = GetCompanyByID(Company.CompanyId);
                if (sup == null)
                {
                    using var context = new BookManagementDBContext();
                    context.Companies.Add(Company);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Company is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Company Company)
        {
            try
            {
                Company sup = GetCompanyByID(Company.CompanyId);
                if (sup != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Companies.Update(Company);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Company does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string CompanyID)
        {
            try
            {
                Company Company = GetCompanyByID(CompanyID);
                if (Company != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Companies.Remove(Company);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Company does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string IdGenerate()
        {
            string newid = null;
            try
            {
                using var context = new BookManagementDBContext();
                Company company = context.Companies.ToList().OrderByDescending(c => c.CompanyId).FirstOrDefault();
                if (company == null)
                {
                    newid = "CM1";
                }
                else
                {
                    newid = company.CompanyId.Substring(0, 2) + (Int32.Parse(company.CompanyId.Substring(2)) + 1).ToString();
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
