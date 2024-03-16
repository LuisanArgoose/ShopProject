using System;
using System.Collections.Generic;
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

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrderConsignment> OrderConsignments { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductConsignment> ProductConsignments { get; set; }

    public virtual DbSet<ProductConsignmentProduct> ProductConsignmentProducts { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<ProductOrderProductInStorage> ProductOrderProductInStorages { get; set; }

    public virtual DbSet<ProductsInStorage> ProductsInStorages { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseProductInStorage> PurchaseProductInStorages { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<RefundProductInStorage> RefundProductInStorages { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RegionPlan> RegionPlans { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalaryType> SalaryTypes { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopPlan> ShopPlans { get; set; }

    public virtual DbSet<ShopPosition> ShopPositions { get; set; }

    public virtual DbSet<ShopType> ShopTypes { get; set; }

    public virtual DbSet<TestTable> TestTables { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorkerType> WorkerTypes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

        var connectionString = configuration.GetConnectionString("ShopProjectDBCode");
        optionsBuilder.UseNpgsql(connectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("Categories_pkey");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.CategoryName).HasColumnName("Category_name");
        });

        modelBuilder.Entity<OrderConsignment>(entity =>
        {
            entity.HasKey(e => e.OrderConsignmentId).HasName("Accepting_orders_pkey");

            entity.ToTable("Order_consignments");

            entity.Property(e => e.OrderConsignmentId)
                .HasDefaultValueSql("nextval('\"Accepting_orders_Consignment_id_seq\"'::regclass)")
                .HasColumnName("Order_consignment_id");
            entity.Property(e => e.DateTime).HasColumnName("Date_time");
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.Worker).WithMany(p => p.OrderConsignments)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Accepting_orders_Worker_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("Payments_pkey");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_id");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.ApprovierWorkerId).HasColumnName("Approvier_worker_id");
            entity.Property(e => e.IsApproved)
                .HasDefaultValue(false)
                .HasColumnName("Is_approved");
            entity.Property(e => e.RecipientWorkerId).HasColumnName("Recipient_worker_id");
            entity.Property(e => e.ShopId).HasColumnName("Shop_id");

            entity.HasOne(d => d.ApprovierWorker).WithMany(p => p.PaymentApprovierWorkers)
                .HasForeignKey(d => d.ApprovierWorkerId)
                .HasConstraintName("Payments_Approvier_worker_id_fkey");

            entity.HasOne(d => d.RecipientWorker).WithMany(p => p.PaymentRecipientWorkers)
                .HasForeignKey(d => d.RecipientWorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Payments_Recipient_worker_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Payments_Shop_id_fkey");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("Positions_pkey");

            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.PositionName).HasColumnName("Position_name");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.SalaryTypeId).HasColumnName("Salary_type_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Positions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("Positions_Role_id_fkey");

            entity.HasOne(d => d.SalaryType).WithMany(p => p.Positions)
                .HasForeignKey(d => d.SalaryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Positions_Salary_type_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Products_pkey");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.BuyCost)
                .HasColumnType("money")
                .HasColumnName("Buy_cost");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.ProductName).HasColumnName("Product_name");
            entity.Property(e => e.SellCost)
                .HasColumnType("money")
                .HasColumnName("Sell_cost");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_Category_id_fkey");
        });

        modelBuilder.Entity<ProductConsignment>(entity =>
        {
            entity.HasKey(e => e.ProductConsignmentId).HasName("Product_consignments_pkey");

            entity.ToTable("Product_consignments");

            entity.Property(e => e.ProductConsignmentId).HasColumnName("Product_consignment_id");
            entity.Property(e => e.DateTime).HasColumnName("Date_time");
            entity.Property(e => e.OrderConsignmentId).HasColumnName("Order_consignment_id");
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.OrderConsignment).WithMany(p => p.ProductConsignments)
                .HasForeignKey(d => d.OrderConsignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_consignments_Order_consignment_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.ProductConsignments)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_consignments_Worker_id_fkey");
        });

        modelBuilder.Entity<ProductConsignmentProduct>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ProductConsignmentId }).HasName("Product_consignment_product_pkey");

            entity.ToTable("Product_consignment_product");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.ProductConsignmentId).HasColumnName("Product_consignment_id");

            entity.HasOne(d => d.ProductConsignment).WithMany(p => p.ProductConsignmentProducts)
                .HasForeignKey(d => d.ProductConsignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_consignment_product_Product_consignment_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductConsignmentProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_consignment_product_Product_id_fkey");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.ProductOrderId).HasName("Product_orders_pkey");

            entity.ToTable("Product_orders");

            entity.Property(e => e.ProductOrderId).HasColumnName("Product_order_id");
            entity.Property(e => e.DateTime).HasColumnName("Date_time");
            entity.Property(e => e.IsApproved).HasColumnName("Is_approved");
            entity.Property(e => e.OrderConsignmentId).HasColumnName("Order_consignment_id");
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.OrderConsignment).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.OrderConsignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_orders_Consigment_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_orders_Worker_id_fkey");
        });

        modelBuilder.Entity<ProductOrderProductInStorage>(entity =>
        {
            entity.HasKey(e => new { e.ProductOrderId, e.ProductInStorageId }).HasName("Product_order_product_in_storage_pkey");

            entity.ToTable("Product_order_product_in_storage");

            entity.Property(e => e.ProductOrderId).HasColumnName("Product_order_id");
            entity.Property(e => e.ProductInStorageId).HasColumnName("Product_in_storage_id");
            entity.Property(e => e.ProductCount).HasColumnName("Product_count");

            entity.HasOne(d => d.ProductInStorage).WithMany(p => p.ProductOrderProductInStorages)
                .HasForeignKey(d => d.ProductInStorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_order_product_in_storage_Product_in_storage_id_fkey");

            entity.HasOne(d => d.ProductOrder).WithMany(p => p.ProductOrderProductInStorages)
                .HasForeignKey(d => d.ProductOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_order_product_in_storage_Product_order_id_fkey");
        });

        modelBuilder.Entity<ProductsInStorage>(entity =>
        {
            entity.HasKey(e => e.ProductInStorageId).HasName("Products_in_storage_pkey");

            entity.ToTable("Products_in_storage");

            entity.Property(e => e.ProductInStorageId).HasColumnName("Product_in_storage_id");
            entity.Property(e => e.ProductCount).HasColumnName("Product_count");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.ShopId).HasColumnName("Shop_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsInStorages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_in_storage_Product_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.ProductsInStorages)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_in_storage_Shop_id_fkey");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("Purchases_pkey");

            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_id");
            entity.Property(e => e.DateTime).HasColumnName("Date_time");
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.Worker).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Purchases_Worker_id_fkey");
        });

        modelBuilder.Entity<PurchaseProductInStorage>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseId, e.ProductsInStorageId }).HasName("Purchase_product_in_storage_pkey");

            entity.ToTable("Purchase_product_in_storage");

            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_id");
            entity.Property(e => e.ProductsInStorageId).HasColumnName("Products_in_storage_id");
            entity.Property(e => e.ProductCount).HasColumnName("Product_count");

            entity.HasOne(d => d.ProductsInStorage).WithMany(p => p.PurchaseProductInStorages)
                .HasForeignKey(d => d.ProductsInStorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Purchase_product_in_storage_Products_in_storage_id_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseProductInStorages)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Purchase_product_in_storage_Purchase_id_fkey");
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.HasKey(e => e.RefundId).HasName("Refunds_pkey");

            entity.Property(e => e.RefundId).HasColumnName("Refund_id");
            entity.Property(e => e.DateTime)
                .HasColumnType("time with time zone")
                .HasColumnName("Date_Time");
            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_id");
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Refunds_Purchase_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Refunds)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Refunds_Worker_id_fkey");
        });

        modelBuilder.Entity<RefundProductInStorage>(entity =>
        {
            entity.HasKey(e => new { e.RefundId, e.ProductInStorageId }).HasName("Refund_product_in_storage_pkey");

            entity.ToTable("Refund_product_in_storage");

            entity.Property(e => e.RefundId).HasColumnName("Refund_id");
            entity.Property(e => e.ProductInStorageId).HasColumnName("Product_in_storage_id");
            entity.Property(e => e.ProductCount).HasColumnName("Product_count");

            entity.HasOne(d => d.ProductInStorage).WithMany(p => p.RefundProductInStorages)
                .HasForeignKey(d => d.ProductInStorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Refund_product_in_storage_Product_in_storage_id_fkey");

            entity.HasOne(d => d.Refund).WithMany(p => p.RefundProductInStorages)
                .HasForeignKey(d => d.RefundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Refund_product_in_storage_Refund_id_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("Regions_pkey");

            entity.Property(e => e.RegionId).HasColumnName("Region_id");
            entity.Property(e => e.RegionName).HasColumnName("Region_name");
        });

        modelBuilder.Entity<RegionPlan>(entity =>
        {
            entity.HasKey(e => e.RegionPlanId).HasName("Region_plans_pkey");

            entity.ToTable("Region_plans");

            entity.Property(e => e.RegionPlanId).HasColumnName("Region_plan_id");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.Profit).HasColumnType("money");
            entity.Property(e => e.RegionId).HasColumnName("Region_id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");

            entity.HasOne(d => d.Region).WithMany(p => p.RegionPlans)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Region_plans_Region_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Roles_pkey");

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.RoleName).HasColumnName("Role_name");
        });

        modelBuilder.Entity<SalaryType>(entity =>
        {
            entity.HasKey(e => e.SalaryTypeId).HasName("Salary_types_pkey");

            entity.ToTable("Salary_types");

            entity.Property(e => e.SalaryTypeId).HasColumnName("Salary_type_id");
            entity.Property(e => e.SalaryTypeName).HasColumnName("Salary_type_name");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("Shops_pkey");

            entity.Property(e => e.ShopId).HasColumnName("Shop_id");
            entity.Property(e => e.RegionId).HasColumnName("Region_id");
            entity.Property(e => e.ShopTypeId).HasColumnName("Shop_type_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Shops)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shops_Region_id_fkey");

            entity.HasOne(d => d.ShopType).WithMany(p => p.Shops)
                .HasForeignKey(d => d.ShopTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shops_Shop_type_id_fkey");
        });

        modelBuilder.Entity<ShopPlan>(entity =>
        {
            entity.HasKey(e => e.ShopPlanId).HasName("Shop_plans_pkey");

            entity.ToTable("Shop_plans");

            entity.Property(e => e.ShopPlanId).HasColumnName("Shop_plan_id");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.Profit).HasColumnType("money");
            entity.Property(e => e.ShopId).HasColumnName("Shop_id");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopPlans)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shop_plans_Shop_id_fkey");
        });

        modelBuilder.Entity<ShopPosition>(entity =>
        {
            entity.HasKey(e => e.ShopPositionId).HasName("Shop_positions_pkey");

            entity.ToTable("Shop_positions");

            entity.Property(e => e.ShopPositionId).HasColumnName("Shop_position_id");
            entity.Property(e => e.PositionId).HasColumnName("Position_id");
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.ShopId).HasColumnName("Shop_id");
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.Position).WithMany(p => p.ShopPositions)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shop_positions_Position_id_fkey");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopPositions)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shop_positions_Shop_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.ShopPositions)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("Shop_positions_Worker_id_fkey");
        });

        modelBuilder.Entity<ShopType>(entity =>
        {
            entity.HasKey(e => e.ShopTypeId).HasName("Shop_types_pkey");

            entity.ToTable("Shop_types");

            entity.Property(e => e.ShopTypeId).HasColumnName("Shop_type_id");
            entity.Property(e => e.ShopTypeName).HasColumnName("Shop_type_name");
        });

        modelBuilder.Entity<TestTable>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("TestTable_pkey");

            entity.ToTable("TestTable");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("Workers_pkey");

            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");
            entity.Property(e => e.WorkerTypeId)
                .HasDefaultValue(1)
                .HasColumnName("Worker_type_id");

            entity.HasOne(d => d.WorkerType).WithMany(p => p.Workers)
                .HasForeignKey(d => d.WorkerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Workers_Worker_type_id_fkey");
        });

        modelBuilder.Entity<WorkerType>(entity =>
        {
            entity.HasKey(e => e.WorkerTypeId).HasName("Worker_types_pkey");

            entity.ToTable("Worker_types");

            entity.Property(e => e.WorkerTypeId).HasColumnName("Worker_type_id");
            entity.Property(e => e.WorkerTypeName).HasColumnName("Worker_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
