using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Common;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = EcommerceAPI.Domain.Entities.File;

namespace EcommerceAPI.Persistence.Contexts
{
    public class EcommerceAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public EcommerceAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            modelBuilder.Entity<Basket>().HasOne(b => b.Order)
                                         .WithOne(o => o.Basket)
                                         .HasForeignKey<Order>(b => b.Id);



            modelBuilder.Entity<Customer>()
                .HasOne(b => b.User)
                .WithOne(o => o.Customer)
                .HasForeignKey<Customer>(b => b.UserId);




            base.OnModelCreating(modelBuilder);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntitiy>();

            foreach (var entry in datas)
            {
                if (entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entry.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
