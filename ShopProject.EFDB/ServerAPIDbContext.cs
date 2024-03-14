using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Collections;
using System.ComponentModel;

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
    

   
}
