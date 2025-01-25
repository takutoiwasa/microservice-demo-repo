using Microsoft.EntityFrameworkCore;
using CompanyManagementService.Models;

namespace CompanyManagementService.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
    }
}