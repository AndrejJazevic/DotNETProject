using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ListingImage> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CouponCode> CouponCodes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Applicant>(entity =>
            {
                entity.HasKey(a => new {a.UserId, a.ItemId});
                
                entity.HasOne(x => x.User)
                    .WithMany(x => x.Applications)
                    .HasForeignKey(x => x.UserId);
                
                entity.HasOne(x => x.User)
                    .WithMany(x => x.Applications)
                    .HasForeignKey(x => x.UserId);
            });

            builder.Entity<User>(entity =>
            {
                entity.HasIndex(x => x.Username).IsUnique();
            });
        }
    }
}