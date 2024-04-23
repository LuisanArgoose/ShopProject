using ShopProject.EFDB.DataModels;
using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.ViewModels.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages.SalesManager
{
    public partial class SMShopsVM : ObservableObject
    {
        public SMShopsVM()
        {
            GetShopsCollectionCommand = new AsyncRelayCommand(GetShopsCollection);
        }

        public IAsyncRelayCommand GetShopsCollectionCommand { get; }

        private async Task GetShopsCollection()
        {
            IsShopCollectionLoading = true;
            var response = await ClientDbProvider.GetShopsCollection();
            if (response.IsSuccessStatusCode == false)
            {
                AlertPoster.PostErrorAlert("Загрузка магазинов", "Не удалось получить данные");
                return;
            }
            var jsonShopsCollection = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<Shop>>(jsonShopsCollection);
            if (result == null)
            {
                AlertPoster.PostSystemErrorAlert("Загрузка магазинов", "Не удалось сериализовать данные");
                return;
            }
            ShopsCollection = result;
            IsShopCollectionLoading = false;
            return;
        }

        [ObservableProperty]
        private bool _isShopCollectionLoading = false;

        [ObservableProperty]
        private ICollection _shopsCollection;

        private Shop? _selectedShop;

        public Shop? SelectedShop
        {
            get => _selectedShop;
            set
            {
                SetProperty(ref _selectedShop, value);
                OnPropertyChanged(nameof(IsShopSelected));
                if(SelectedShop != null)
                {
                    if(SelectedShopVM == null)
                    {
                        SelectedShopVM = new ShopVM((int)SelectedShop.ShopId);
                    }
                    else
                    {
                        SelectedShopVM.ShopId = (int)SelectedShop.ShopId;
                    }
                    
                }
                    
            }
        }

        public bool IsShopSelected { get => SelectedShop != null;}

        [ObservableProperty]
        private ShopVM _selectedShopVM;


    }
}
