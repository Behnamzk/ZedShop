using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.DataLayer.Context
{
    public class ZedShopContext:DbContext
    {
        public ZedShopContext(DbContextOptions<ZedShopContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductRate> Rates { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAccess> RolesAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region EnumGenderUser
            modelBuilder
            .Entity<User>()
            .Property(e => e.gender)
            .HasConversion<int>();
            #endregion


            #region ManyToMany ProductCategory

            modelBuilder.Entity<ProductCategory>().HasKey(b => new { b.ProductId, b.CategoryId });

            modelBuilder.Entity<ProductCategory>().HasOne(b => b.Product)
                .WithMany(b => b.ProductCategories).HasForeignKey(b => b.ProductId);

            modelBuilder.Entity<ProductCategory>().HasOne(b => b.Category)
                .WithMany(b => b.ProductCategories).HasForeignKey(b => b.CategoryId);

            #endregion

            #region ManyToMany OrderProduct

            modelBuilder.Entity<OrderProduct>().HasKey(b => new { b.ProductId, b.OrdrId });

            modelBuilder.Entity<OrderProduct>().HasOne(b => b.Product)
                .WithMany(b => b.OrderProducts).HasForeignKey(b => b.ProductId);

            modelBuilder.Entity<OrderProduct>().HasOne(b => b.Order)
                .WithMany(b => b.OrderProducts).HasForeignKey(b => b.OrdrId);

            #endregion

            #region ManyToMany RoleAccess

            modelBuilder.Entity<RoleAccess>().HasKey(b => new { b.RoleId, b.AccessId });

            modelBuilder.Entity<RoleAccess>().HasOne(b => b.Role)
                .WithMany(b => b.RoleAccesses).HasForeignKey(b => b.RoleId);

            modelBuilder.Entity<RoleAccess>().HasOne(b => b.Access)
                .WithMany(b => b.RoleAccesses).HasForeignKey(b => b.AccessId);

            #endregion

        }
    }

    
}
