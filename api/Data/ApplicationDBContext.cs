using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolios> Portfolios {get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolios>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));

            builder.Entity<Portfolios>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolios>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);

            // Predefined roles: Admin and User
            List<IdentityRole> roles = new()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            // Seed roles into the database
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}