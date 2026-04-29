using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductInBasket> ProductInBaskets { get; set; }
        public DbSet<User> Users { get; set; }

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

            modelBuilder.Entity<User>(o =>
            {
                o.HasKey(e => new {e.Id, e.UserName}); //Radau kelis variantus
                    //kaip db stulpeliui suteikti unikalią reikšmę, tai .HasKey(),
                    //.IsUnique(), ir rodos dar trečias buvo. Ar kažkuris šitu atvėju
                    //yra geresnis? O gal nebūtina apskritai dėti unique, jei User.Add()
                    //metode tikrinama ar jau toks use name egzistuoja?
                o.Property(e => e.Password).IsRequired();
            });
        }
    }
}