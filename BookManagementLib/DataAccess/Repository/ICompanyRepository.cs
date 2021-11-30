using BookManagementLib.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompanyByID(string CompanyId);
        Company GetCompanyByName(string CompanyName);
        void InsertCompany(Company Company);
        void DeleteCompany(string CompanysId);
        void UpdateCompany(Company Company);
        string CompanyIdGenerate();
    }
}
