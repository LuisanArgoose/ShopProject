using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShopProject.EFDB.Models;

public partial class ShopProjectDbContext : DbContext
{
    public ShopProjectDbContext()
    {

    }

    public ShopProjectDbContext(DbContextOptions<ShopProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cashier> Cashiers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPlan> ProductPlans { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopPlan> ShopPlans { get; set; }

    public virtual DbSet<TokenLogin> TokenLogins { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkerPlan> WorkerPlans { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {


        var connectionString = "Host=localhost;Port=5432;Database=ShopProjectDBNewCode;Username=ShopProject.API;Password=Underware";
        optionsBuilder.UseNpgsql(connectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<TokenLogin>(entity =>
        {
            entity.HasKey(e => e.TokenLoginId).HasName("TokenLogins_pkey");

        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Roles_pkey");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_pkey"); ;

            entity.Navigation(e => e.Role).AutoInclude();
            //entity.Navigation(e => e.Shops).AutoInclude();

        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("Shops_pkey");

            //entity.Navigation(e => e.ShopPlans).AutoInclude();
            //entity.Navigation(e => e.ProductPlans).AutoInclude();
            //entity.Navigation(e => e.WorkerPlans).AutoInclude();
            //entity.Navigation(e => e.Cashiers).AutoInclude();
            //entity.Navigation(e => e.Users).AutoInclude();

        });

        modelBuilder.Entity<WorkerPlan>(entity =>
        {
            entity.HasKey(e => e.WorkerPlanId).HasName("WorkerPlans_pkey");

            entity.Navigation(e => e.Shop).AutoInclude();
        });

        modelBuilder.Entity<ShopPlan>(entity =>
        {
            entity.HasKey(e => e.ShopPlanId).HasName("ShopPlans_pkey");

            entity.Navigation(e => e.Shop).AutoInclude();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Products_pkey");

            //entity.Navigation(e => e.ProductPlans).AutoInclude();
            //entity.Navigation(e => e.PurchaseProducts).AutoInclude();
        });

        modelBuilder.Entity<ProductPlan>(entity =>
        {
            entity.HasKey(e => e.ProductPlanId).HasName("ProductPlans_pkey");

            entity.Navigation(e => e.Shop).AutoInclude();
            entity.Navigation(e => e.Product).AutoInclude();
        });

        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.HasKey(e => e.CashierId).HasName("Cashiers_pkey");

            //entity.Navigation(e => e.Shops).AutoInclude();
            //entity.Navigation(e => e.Purchases).AutoInclude();
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("Purchases_pkey");

            entity.Navigation(e => e.Cashier).AutoInclude();
            //entity.Navigation(e => e.PurchaseProducts).AutoInclude();
        });

        modelBuilder.Entity<PurchaseProduct>(entity =>
        {
            entity.HasKey(e => e.PurchaseProductId).HasName("PurchaseProducts_pkey");

            entity.Navigation(e => e.Purchase).AutoInclude();
            entity.Navigation(e => e.Product).AutoInclude();
        });

        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
