using BlogApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
