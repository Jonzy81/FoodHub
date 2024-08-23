using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FoodHub.Data
{
    public class RestaurantContext : DbContext
    {
        
            public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
            {

            }
            public DbSet<Restaurant> Restaurant { get; set; }
            public DbSet<Menu> Menu { get; set; }
            public DbSet<Customer> Customer { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
                modelBuilder.Entity<Menu>().ToTable("Menu");
                modelBuilder.Entity<Customer>().ToTable("Customer");
            }
        
    }
}
