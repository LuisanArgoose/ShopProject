using ShopProject.UI.ViewModels.Pages.Examples;
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
            var shopId = Settings.GetActiveUser().ShopId;
            if(shopId != null)
                SelectedShop = new ShopVM((int)shopId);
        }
        [ObservableProperty]
        private ShopVM _selectedShop;
    }
}
