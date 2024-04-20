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

    public virtual DbSet<PlanAtribute> PlanAtributes { get; set; }

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
        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.Property(p => p.OperationTime).HasColumnType("timestamp without time zone");

           
        });

        modelBuilder.Entity<ShopPlan>(entity =>
        {
            entity.Property(p => p.SetTime).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<Shop>(entity =>
        {

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
