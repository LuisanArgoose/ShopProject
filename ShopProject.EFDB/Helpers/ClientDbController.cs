using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopProject.EFDB.Helpers
{
    public class ClientDbController
    {
        private readonly HttpClient _httpClient;
        public ClientDbController(string baseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }
        public async Task<IEnumerable<object>?> GetEntitiesAsync(string tableName, Type entityType)
        {
            var responseEntityCollection = await _httpClient.GetAsync(tableName);
            if (responseEntityCollection.IsSuccessStatusCode)
            {
                var jsonEntityCollection = await responseEntityCollection.Content.ReadAsStringAsync();
                var entityList = JsonConvert.DeserializeObject(jsonEntityCollection, typeof(List<>).MakeGenericType(entityType)) as IEnumerable<object>;
                return entityList;
            }
            else
            {
                return null;
            }
        }
    }
}
