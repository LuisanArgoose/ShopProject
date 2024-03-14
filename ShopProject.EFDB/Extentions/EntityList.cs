using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using ShopProject.EFDB.Helpers;
using ShopProject.EFDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Extentions
{
    public class EntityList<T> :  BindingList<T>, IEntityList
    {

        public EntityList() { }
        public async Task Fill()
        {
            Clear();
            var response = await ClientDbProvider.GetEntitiesAsync(typeof(T));
            if(!response.IsSuccessStatusCode)
            {
                return;
            }
            var collectionJson = await response.Content.ReadAsStringAsync();
            var collection = JsonSerializer.Deserialize<List<T>>(collectionJson, JsonOptions.GetOptions());
            if (collection != null)
            {
                RaiseListChangedEvents = false;
                
                foreach (var item in collection)
                {
                    Add(item);
                }
                RaiseListChangedEvents = true;
                ResetBindings();
            }

            
        }
        protected async override void OnListChanged(ListChangedEventArgs e)
        {

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    {
                        var entity = this[e.NewIndex];
                        if (entity == null)
                        {
                            return;
                        }
                        var response = await ClientDbProvider.PostCUD(entity, "Create");
                        if (!response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        var newItemJson = await response.Content.ReadAsStringAsync();
                        RaiseListChangedEvents = false;
                        var indexedEntity = JsonSerializer.Deserialize<T>(newItemJson, JsonOptions.GetOptions()) ?? throw new Exception("Serialize fail");
                        this[e.NewIndex] = indexedEntity;
                        RaiseListChangedEvents = true;
                        ResetBindings();
                        break;
                    }
                case ListChangedType.ItemDeleted:
                    {
                        var entity = this[e.NewIndex];
                        if (entity == null)
                        {
                            return;
                        }
                        var response = await ClientDbProvider.PostCUD(entity, "Delete");
                        if (response.IsSuccessStatusCode)
                        {
                            RaiseListChangedEvents = false;
                            base.RemoveItem(e.NewIndex);
                            RaiseListChangedEvents = true;
                            ResetBindings();
                        }

                        
                        break;
                    }
                case ListChangedType.ItemChanged:
                    {
                        var entity = this[e.NewIndex];
                        if (entity == null)
                        {
                            return;
                        }
                        var response = await ClientDbProvider.PostCUD(entity, "Update");
                        if (!response.IsSuccessStatusCode)
                        {
                            return;
                        }
                        var newItemJson = await response.Content.ReadAsStringAsync();
                        RaiseListChangedEvents = false;
                        var indexedEntity = JsonSerializer.Deserialize<T>(newItemJson, JsonOptions.GetOptions()) ?? throw new Exception("Serialize fail");
                        this[e.NewIndex] = indexedEntity;
                        RaiseListChangedEvents = true;
                        ResetBindings();
                        break;
                    }
            }
            base.OnListChanged(e);
        }
        protected override void RemoveItem(int index)
        {
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
            
        }
    }
}
