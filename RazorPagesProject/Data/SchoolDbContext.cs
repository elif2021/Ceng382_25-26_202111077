using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Models;

namespace RazorPagesProject.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }  // User tablosunu ekliyoruz
        public DbSet<Class> Classes { get; set; }
    }
}
