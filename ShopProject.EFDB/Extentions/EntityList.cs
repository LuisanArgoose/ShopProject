using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopProject.EFDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Extentions
{
    public class EntityList<T> :  BindingList<T>
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public EntityList() { }
        public async Task Fill()
        {
            
            var collectionJson = await ClientDbProvider.GetEntitiesAsync(typeof(T));
            var collection = JsonSerializer.Deserialize<List<T>>(collectionJson, _options);
            if (collection != null)
            {
                RaiseListChangedEvents = false;
                foreach (var item in collection)
                {
                    Add(item);
                }
                RaiseListChangedEvents = true;

            }


        }
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            
            base.OnListChanged(e);
        }
        protected override async void InsertItem(int index, T item)
        {
            var newItem = await ClientDbProvider.PostCUD(item, "Create");
            item = JsonSerializer.Deserialize<T>(newItem, _options);
            base.InsertItem(index, item);
        }
    }
}
