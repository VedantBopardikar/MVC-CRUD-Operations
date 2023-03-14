using Fullstack.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.api.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        
    }
}
