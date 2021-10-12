using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.Repository
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanys();
        Company GetCompanyByID(string CompanyId);
        Company GetCompanyByName(string CompanyName);
        void InsertCompany(Company Company);
        void DeleteCompany(string CompanysId);
        void UpdateCompany(Company Company);
        string CompanyIdGenerate();
    }
}
