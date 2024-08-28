using FoodHub.Model;
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
        public DbSet<Table> Restaurant { get; set; }
        public DbSet<MenuItem> Menu { get; set; }
        public DbSet<User> Customer { get; set; }
        public DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>().ToTable("Restaurant");
            modelBuilder.Entity<MenuItem>().ToTable("Menu");
            modelBuilder.Entity<User>().ToTable("Customer");
            modelBuilder.Entity<Booking>().ToTable("Booking");
        }

    }
}
