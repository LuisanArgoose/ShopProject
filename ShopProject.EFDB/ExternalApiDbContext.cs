using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
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
            FillCollections();
        }
        public async void FillCollections()
        {
            var tables = GetType().GetProperties().Where(x => x.PropertyType.Name == "DbSet`1");
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
                
            }
            

        }
    }
}
