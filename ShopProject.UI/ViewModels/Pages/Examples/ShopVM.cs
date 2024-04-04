using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.UI.Services;

namespace ShopProject.UI.ViewModels.Pages.Examples
{
    public partial class ShopVM : ObservableObject
    {
        private CacheStorageService _cacheStorageService;
        public ShopVM(CacheStorageService cacheStorageService)
        {
            _cacheStorageService = cacheStorageService;
            SelectedShop = cacheStorageService.SelectedShop;
        }

        [ObservableProperty]
        private Shop _selectedShop;
    }
}
