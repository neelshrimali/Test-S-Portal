using Microsoft.EntityFrameworkCore;
using Test_S_Portal.Models.Entities;

namespace Test_S_Portal.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
            
        }

        public DbSet<StudentTest> TestStudents { get; set; }

    }
}
