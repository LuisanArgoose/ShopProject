using ShopProject.UI.ViewModels.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages.Manager
{
    public partial class ManagerVM : ObservableObject
    {
        public ManagerVM()
        {

            SelectedShop = Settings.GetActiveUser().Shop;
        }


        [ObservableProperty]
        private Shop _selectedShop;

        [ObservableProperty]
        private ShopVM _selectedShopVM;

        public void SelectShopWithUser()
        {
            
            if (SelectedShop != null)
                SelectedShopVM = new ShopVM((int)SelectedShop.ShopId);
        }
    }
}
