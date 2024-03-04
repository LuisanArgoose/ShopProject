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
        public static event EventHandler OnFillEvent;

        public static async Task Fill<T>(this DbSet<T> dbSet) where T : class
        {
            
            var clientController = ClientDbProvider.GetInstance();
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
                OnFillEvent?.Invoke(null, EventArgs.Empty);
            }
            

        }

    }
}
