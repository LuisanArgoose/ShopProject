﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityFrameworkCore.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ShopProject.EFDB.Helpers;


namespace ShopProject.EFDB
{
    public class ClientAPIDbContext : ServerAPIDbContext
    {

        private readonly ClientDbController _clientDbController;
        public ClientAPIDbContext(string baseAddress)
        {
            _clientDbController = new ClientDbController(baseAddress);
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