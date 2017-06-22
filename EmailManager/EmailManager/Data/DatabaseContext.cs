using EmailManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmailManager.Data
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<EnEvent> EnEvents { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);
            builder.Entity<EnEvent>();
        }
    }
}
