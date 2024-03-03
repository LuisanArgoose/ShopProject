using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        public static async Task FillAsync<T>(this DbSet<T> dbSet) where T : class
        {
            

            var dbSetEntityType = dbSet.GetType().GetGenericArguments()[0];

            var entitiesList = await _context.ClientDbController.GetEntitiesAsync(dbSetEntityType);

            dbSet.AddRange(entitiesList as T);
        }
    }
}
