using e_commerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Context
{
    public class StoreContext:IdentityDbContext<ApplicationUser>

    {
        public StoreContext()
        {
            
        }
        public StoreContext(DbContextOptions options):base(options)
        {
            
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//composite Key

            modelBuilder.Entity<CartItems>().HasKey(ci => new { ci.CartId, ci.ProductId });
            modelBuilder.Entity<FavouriteItems>().HasKey(c => new { c.FavouriteId, c.ProductId });
            base.OnModelCreating(modelBuilder);

           
            //modelBuilder.Entity<CartItems>()
            //    .HasOne(ci => ci.Cart)
            //    .WithMany(c => c.CartItems)
            //    .HasForeignKey(ci => ci.CartId);

            //modelBuilder.Entity<CartItems>()
            //    .HasOne(ci => ci.Product)
            //    .WithMany(p => p.CartItems)
            //    .HasForeignKey(ci => ci.ProductId);
        }

        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Favourite> Favorite { get; set; }
        public DbSet<FavouriteItems> FavoriteItems { get; set; }

    }
}
