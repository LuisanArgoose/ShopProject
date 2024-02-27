using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using ShopProject.EFDB.Models;
using System;
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
            //FillCollections();
        }
        private struct PropData
        {
            public MethodInfo? MethodInfo { get; set; }
            public string Name { get; set; }
        }

        public async void FillCollections()
        {
            

            var tables = GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
            List<PropData> properties = tables
                .Select(p => new PropData
                {
                    MethodInfo = p.GetGetMethod(),
                    Name = p.Name
                })
                .ToList();

            foreach(var property in properties)
            {
                var response = await _httpClient.GetAsync(property.Name);
                if (response.IsSuccessStatusCode)
                {
                    var data =  await response.Content.ReadAsStringAsync();

                    var dbSet = property.MethodInfo.Invoke(this, null);
                    Type entityType = dbSet.GetType().GetGenericArguments()[0];
                    var entityList = JsonSerializer.Deserialize(data, typeof(List<>).MakeGenericType(entityType)) as IEnumerable<object>;
                    (dbSet as ICollection<object>).ToList().AddRange(entityList);
                }
            }
            SaveChanges();
            /*
            foreach (var table in tables)
            {
                var response = await _httpClient.GetAsync(table.Name);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var entityList = JsonConvert.DeserializeObject<dynamic>(data);
                    var result = table.GetValue(this);
                    var gkd = (result as DbSet<Category>).Local.ToList();

                    
                }
                
            }*/


        }
    }
}
