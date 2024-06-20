using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EportfolioRestAPI.Entities
{
    public class PortolioDbContext : IdentityDbContext<IdentityUser>
    {
        public PortolioDbContext(DbContextOptions<PortolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Portofolio> Portofolios { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<Information> Informations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portofolio>()
                .Property(r => r.PortofolioName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Information>()
                .Property(d => d.AboutMe)
                .IsRequired();
        }
    }
}
