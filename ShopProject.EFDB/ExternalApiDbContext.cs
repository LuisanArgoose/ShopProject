using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityFrameworkCore.Extensions;
using Newtonsoft.Json;
using ShopProject.EFDB.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace ShopProject.EFDB
{
    public class ExternalApiDbContext : ProjectShopDbContext
    {

        private readonly ClientDbController _clientDbController;
        public ExternalApiDbContext(string baseAddress)
        {
            _clientDbController = new ClientDbController(baseAddress);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ExternalApiDataBase");
            
        }
        public async Task FillCollections()
        {
            
                var properties = GetType().GetProperties()
                    .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var property in properties)
            {
                var dbSet = property.GetValue(this);
                if (dbSet == null)
                    continue;
                Type entityType = dbSet.GetType().GetGenericArguments()[0];
                var entityList = await _clientDbController.GetEntitiesAsync(property.Name, entityType);
                if(entityList != null)
                {
                    await AddRangeAsync(entityList);
                }
                
            }

            await SaveChangesAsync();
            return;
        }
        
    }
}
