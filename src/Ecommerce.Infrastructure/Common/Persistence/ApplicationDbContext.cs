using Ecommerce.Domain.catgory;
using Ecommerce.Domain.identity;
using Ecommerce.Domain.order;
using Ecommerce.Domain.orderItem;
using Ecommerce.Domain.product;
using Ecommerce.Domain.review;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Description)
                      .HasMaxLength(500);

                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.OrderItems)
                      .WithOne(oi => oi.Product)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.OrderTotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(o => o.OrderDate)
                      .IsRequired();

                entity.HasOne(o => o.ApplicationUser)
                     .WithMany(u => u.orders)
                     .HasForeignKey(o => o.ApplicationUserId)
                     .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(o => o.OrderItems)
                     .WithOne(oi => oi.Order)
                     .HasForeignKey(oi => oi.OrderId)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.ReviewId);

                entity.Property(r => r.Comment)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(r => r.Rating)
                    .IsRequired()
                    .HasDefaultValue(1);

                entity.Property(r => r.ReviewDate)
                    .IsRequired();

                entity.HasOne(r => r.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.ApplicationUser)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => new { oi.OrderId, oi.ProductId });

                entity.Property(oi => oi.Quantity)
                    .IsRequired();

                entity.Property(oi => oi.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApplicationUser>()
                        .ToTable("Users");
            modelBuilder.Entity<IdentityRole>()
                        .ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>()
                        .ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>()
                        .ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>()
                        .ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>()
                        .ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>()
                        .ToTable("UserTokens");

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}