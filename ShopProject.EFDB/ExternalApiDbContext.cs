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


namespace ShopProject.EFDB
{
    public class ExternalApiDbContext : ProjectShopDbContext
    {

        private readonly HttpClient _httpClient;
        public ExternalApiDbContext(HttpClient httpClient, string baseAddress)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseAddress);
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
                var response = await _httpClient.GetAsync(property.Name);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    var dbSet = property.GetValue(this);

                    Type entityType = dbSet.GetType().GetGenericArguments()[0];
                    var entityList = JsonConvert.DeserializeObject(data, typeof(List<>).MakeGenericType(entityType)) as IEnumerable<object>;
                    
                    await this.AddRangeAsync(entityList);
                }
            }

            await SaveChangesAsync();
            return;
        }
        
    }
}
