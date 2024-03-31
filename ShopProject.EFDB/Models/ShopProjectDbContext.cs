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

    

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<TokenLogin> TokenLogins { get; set; }


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

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("Shops_pkey");
            entity.HasOne(e => e.User)
                .WithMany(e => e.Shops)
                .HasForeignKey(e => e.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_pkey"); ;

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Role_id_fkey");


            entity.Navigation(e => e.Role).AutoInclude();
            entity.Navigation(e => e.Shops).AutoInclude();


        });

        

            OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
