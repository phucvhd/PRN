using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public Company GetCompanyByID(string CompanyId) => CompanyDAO.Instance.GetCompanyByID(CompanyId);
        public Company GetCompanyByName(string CompanyName) => CompanyDAO.Instance.GetCompanyByName(CompanyName);
        public IEnumerable<Company> GetCompanies() => CompanyDAO.Instance.GetCompanyList();
        public void InsertCompany(Company Company) => CompanyDAO.Instance.Insert(Company);
        public void DeleteCompany(string CompanyId) => CompanyDAO.Instance.Remove(CompanyId);
        public void UpdateCompany(Company Company) => CompanyDAO.Instance.Update(Company);
        public string CompanyIdGenerate() => CompanyDAO.Instance.IdGenerate();
    }
}
