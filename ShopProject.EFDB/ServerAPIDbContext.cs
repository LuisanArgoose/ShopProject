using System;
using System.Collections.Generic;
using ShopProject.EFDB.Models;
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
        DbCollectionsInit();
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
    
    private void DbCollectionsInit()
    {
        Task.Run(() =>
        {
            _dbCollections.Add(typeof(TestTable), TestTables);
        });
        
    }
    private readonly Dictionary<Type, IListSource> _dbCollections = [];
    public string GetDbCollectionJson(string name)
    {

        return _dbCollections.First(x => x.Key.Name == name).Value.GetList().GetType().Name;
    }
}
