using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductInBasket> ProductInBaskets { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(e => e.Name).IsRequired();
                o.Property(e => e.Price).IsRequired();
            });

            modelBuilder.Entity<Basket>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(e => e.UserId).IsRequired();
                o.HasMany(e => e.ProductInBaskets)
                    .WithOne(e => e.Basket)
                    .HasForeignKey(e => e.BasketId).
                    IsRequired();
            });

            modelBuilder.Entity<ProductInBasket>(o =>
            {
                o.HasKey(e => e.Id);
                o.HasOne(e => e.Product)
                    .WithMany(e => e.ProductInBaskets)
                    .HasForeignKey(e => e.ProductId);
                o.Property(e => e.ProductId).IsRequired();
                o.Property(e => e.Count).IsRequired();
            });
        }
    }
}