using Microsoft.EntityFrameworkCore;
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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml;


namespace ShopProject.EFDB
{
    public class ClientAPIDbContext : ServerAPIDbContext
    {

        private readonly ClientDbController _clientDbController;
        public ClientAPIDbContext(string baseAddress)
        {
            _clientDbController = ClientDbController.GetInstance();
            
        }
        public ClientDbController ClientDbController { get => _clientDbController; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ExternalApiDataBase");
            
        }
        public async Task FillAllCollections(string collectionName = "All")
        {
            
                var properties = GetType().GetProperties()
                    .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
            if(collectionName == "All")
            {
                foreach (var property in properties)
                {
                    await FillCollection(property);
                }
            }
            else
            {
                var property = properties.First(p => p.Name == collectionName);
                await FillCollection(property);

            }
            

            await SaveChangesAsync();
            return;
        }
        
        // Fills the collection by property info
        private async Task FillCollection(PropertyInfo? property)
        {
            if (property == null)
                return;
            var dbSet = property.GetValue(this);
            if (dbSet == null)
                return;
            Type entityType = dbSet.GetType().GetGenericArguments()[0];
            var entityList = await _clientDbController.GetEntitiesAsync(entityType);
            if (entityList != null && dbSet != null)
            {
                ClearDbSet(dbSet as IEnumerable<object>);
                await AddRangeAsync(entityList);
            }
        }
        private void ClearDbSet(IEnumerable<object>? dbSet)
        {
            if(dbSet == null)
                return;
            foreach(var entity in dbSet)
            {
                Entry(entity).State = EntityState.Deleted;
            }
        }

        public void SaveData()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //Create(entry.Entity as MyEntity);
                        break;
                    case EntityState.Deleted:
                        //Delete(entry.Entity.Id);
                        break;
                    case EntityState.Modified:
                        //Update(entry.Entity as MyEntity);
                        break;
                }
            }
        }


    }
}
