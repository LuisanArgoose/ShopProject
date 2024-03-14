using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.EFDB.Extentions;
using System.Security.Principal;

namespace ShopProject.UI.ViewModels.Pages
{
    public partial class ProfileVM : ObservableObject
    {
        [ObservableProperty]
        private IEntityList _table;

        [ObservableProperty]
        private BindingList<string> _tablesName  = [];

        public ProfileVM()
        {
            LoadEntitiesNameCommand = new AsyncRelayCommand(LoadEntitiesName);

        }

        public IAsyncRelayCommand LoadEntitiesNameCommand { get; }

        private async Task LoadEntitiesName()
        {
            var response = await ClientDbProvider.GetEntitiesNameAsync();
            if (response.IsSuccessStatusCode)
            {
                var collectionJson = await response.Content.ReadAsStringAsync();
                var collection = JsonSerializer.Deserialize<List<string>>(collectionJson, JsonOptions.GetOptions());
                if (collection == null)
                    return;
                TablesName.Clear();
                foreach ( var item in collection)
                {
                    TablesName.Add(item);
                }
            }
            
        }
        private string _selectedTable;
        public string SelectedTable
        {
            get { return _selectedTable; }
            set 
            { 
                SetProperty(ref _selectedTable, value);
                LoadTable();
            }
        }

        //public IRelayCommand LoadTableCommand { get; }
        private void LoadTable()
        {
            if (SelectedTable == null)
                return;
            Table = new EntityList<TestTable>();
            Table.Fill();
        }
    }
}
