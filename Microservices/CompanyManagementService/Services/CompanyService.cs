using CompanyManagementService.Data;
using CompanyManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementService.Services
{
    public class CompanyService
    {
        private readonly CompanyContext _context;

        public CompanyService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }
    }
}