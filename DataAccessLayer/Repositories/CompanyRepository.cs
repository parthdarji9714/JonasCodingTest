using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            var companies = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
            return companies?.FirstOrDefault();
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            var itemRepo = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode)))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return _companyDbWrapper.Update(itemRepo);
            }

            return _companyDbWrapper.Insert(company);
        }
        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            // Create an expression to find the company to delete
            Expression<Func<Company, bool>> predicate = c => c.CompanyCode.Equals(companyCode);

            // Try to find the company to delete
            var companies = await _companyDbWrapper.FindAsync(predicate);
            var companyToDelete = companies.FirstOrDefault();

            if (companyToDelete == null)
            {
                return false; // Company not found
            }

            // Delete the company using the predicate expression
            return await _companyDbWrapper.DeleteAsync(predicate);
        }

    }
}
