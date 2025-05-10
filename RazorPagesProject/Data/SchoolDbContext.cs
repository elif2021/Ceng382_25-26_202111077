<<<<<<< HEAD
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
        

        public DbSet<Class> Classes { get; set; }
    }
}
=======
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
        

        public DbSet<Class> Classes { get; set; }
    }
}
>>>>>>> d8e6c045f527c1d62f8005088d1482960b12b32c
