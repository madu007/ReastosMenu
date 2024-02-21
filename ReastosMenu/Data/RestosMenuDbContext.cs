using Microsoft.EntityFrameworkCore;
using ReastosMenu.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ReastosMenu.Data
{
    public class RestosMenuDbContext : DbContext
    {
        public RestosMenuDbContext(DbContextOptions<RestosMenuDbContext> options) : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuImage> MenuImages { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BannerImage> BannerImages { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
