using Microsoft.EntityFrameworkCore;

namespace ProductAndServices.Models
{
    public partial class DatabaseContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=ProductService;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e =>e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e =>e.Description).IsRequired().HasMaxLength(250);
                entity.Property(e =>e.Price).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });
        }
    }
}
