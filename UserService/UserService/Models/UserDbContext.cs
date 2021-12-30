using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public partial class UserDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Corporate> Corporators { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=UserService;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Username).IsUnicode(false).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsUnicode(false).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(150);
                entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Mobile).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.IsPersonal).IsRequired().HasColumnType("bit");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("Administrator");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100).IsUnicode(false);
            });

            modelBuilder.Entity<Corporate>(entity =>
            {
                entity.ToTable("Corporate");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.CreatedAt).IsRequired().HasColumnType("datetime");
                entity.Property(e => e.Pib).IsRequired().HasMaxLength(10);
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CompanyCity).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CompanyAddress).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CompanyEmail).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CompanyMobile).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Corporate>()
                .HasOne(e => e.Owner)
                .WithMany(e => e.CorporateAccounts);

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.ToTable("Personal");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Username).IsUnicode(false).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsUnicode(false).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(150);
                entity.Property(e => e.City).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Mobile).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.IsPersonal).IsRequired().HasColumnType("bit");
                entity.Property(e => e.CreatedAt).IsRequired().HasColumnType("datetime");
                entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18, 2)");
            });
        }
    }
}
