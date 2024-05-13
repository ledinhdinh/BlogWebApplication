using BlogWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApplication.Services
{
    // Class ApplicationDbContext inherited DbContext.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Blog> BlogWebs { get; set; }
        public DbSet<Category> Categorys { get; set; }
    }
}
