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
            
        }

        [ObservableProperty]
        private ShopVM _selectedShopVM;

        public void SelectShopWithUser()
        {
            var shopId = Settings.GetActiveUser().ShopId;
            if (shopId != null)
                SelectedShopVM = new ShopVM((int)shopId);
        }
    }
}
