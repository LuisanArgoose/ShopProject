using System;
using System.Collections.Generic;
using ShopProject.EFDB.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Collections;

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

    public IEnumerable GetDbSet(Type dbSetEntityType)
    {
        var dbSetProperty = GetType().GetProperties()
            .FirstOrDefault(p => p.PropertyType.IsGenericType
                                && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                                && p.PropertyType.GetGenericArguments()[0] == dbSetEntityType);

        if (dbSetProperty != null)
        {
            var dbSet = dbSetProperty.GetValue(this) as IEnumerable;
            return dbSet;
        }
        else
        {
            // Обработка ошибки в случае, если не удалось найти соответствующий Dbset
            return null;
        }
    }
}
