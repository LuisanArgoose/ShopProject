using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Helpers
{
    public static class DbSetFillExtention
    {
        private static ClientAPIDbContext _context;
        public static void SetContext(ClientAPIDbContext context)
        {
            _context = context;
        }

        public static async Task Fill<T>(this DbSet<T> dbSet) where T : class
        {
            
            var clientController = ClientDbController.GetInstance();
            var collectionJson = await clientController.GetEntitiesAsync(dbSet.EntityType.ClrType);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var collection = JsonSerializer.Deserialize<List<T>>(collectionJson,options);
            if (collection != null)
            {
                dbSet.Local.Clear();
                dbSet.AddRange(collection);
                await _context.SaveChangesAsync();
            }
            

        }

    }
}
