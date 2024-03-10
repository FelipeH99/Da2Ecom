using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DbSet<AdminToken> AdminTokens { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandDiscount> BrandDiscounts { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorDiscount> ColorDiscounts { get; set; }
        public DbSet<PercentageDiscount> PercentageDiscounts { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<QuantityDiscount> QuantityDiscounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }


        public DataBaseContext()
        { 
        }
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString(@"ECommerce");
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Brand>().HasKey(b => b.Id);
            modelBuilder.Entity<Color>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().HasKey(pro => pro.Id);
            modelBuilder.Entity<Purchase>().HasKey(pu => pu.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<PercentageDiscount>().HasKey(pd => pd.Id);
            modelBuilder.Entity<QuantityDiscount>().HasKey(qd => qd.Id);
            modelBuilder.Entity<BrandDiscount>().HasKey(bd => bd.Id);
            modelBuilder.Entity<ColorDiscount>().HasKey(cd => cd.Id);
            modelBuilder.Entity<Permission>().HasKey(per => per.Id);
            modelBuilder.Entity<AdminToken>().HasKey(adT => adT.Id);
            modelBuilder.Entity<PaymentMethod>().HasKey(pm => pm.Id);


        }
    }
}
