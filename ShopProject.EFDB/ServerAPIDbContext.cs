using System;
using System.Collections.Generic;
using ShopProject.EFDB.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ShopProject.EFDB;

public partial class ServerAPIDbContext : ShopProjectDbContext
{
    public ServerAPIDbContext()
    {
    }

    public ServerAPIDbContext(DbContextOptions<ShopProjectDbContext> options)
        : base(options)
    {
    }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

        var connectionString = configuration.GetConnectionString("ShopProjectDB");
        optionsBuilder.UseNpgsql(connectionString);


    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
