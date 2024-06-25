using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EportfolioRestAPI.Models;

namespace EportfolioRestAPI.Data
{
    public class PortfolioDbContext : IdentityDbContext<IdentityUser>
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<PortfolioModel> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PortfolioModel>()
                .Property(p => p.PortfolioName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<PortfolioModel>()
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<PortfolioModel>()
                .Property(p => p.YouTubeLink)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<PortfolioModel>()
                .Property(p => p.Username)
                .IsRequired();
        }
    }
}
