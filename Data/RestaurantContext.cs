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
        public DbSet<Table> Tables { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Table>().ToTable("Restaurant");
            //modelBuilder.Entity<MenuItem>().ToTable("Menu");
            //modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<Booking>().ToTable("Booking");
        }
    }
}
