using ShopProject.EFDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models
{
    public partial class ShopPlanModel : ObservableObject
    {
        public event EventHandler DeleteShopPlanEvent;
        public ShopPlanModel(ShopPlan plan, EventHandler deleteShopPlanEvent)
        {
            ShopPlan = plan;
            DeleteShopPlanEvent += deleteShopPlanEvent;
        }

        [ObservableProperty]
        private ShopPlan _shopPlan;

        [RelayCommand]
        private async Task DeleteShopPlan()
        {
            await ClientDbProvider.DeleteShopPlan(ShopPlan.ShopPlanId);
            OnDeleteShopPlan();
        }
        private void OnDeleteShopPlan()
        {
            DeleteShopPlanEvent?.Invoke(this, EventArgs.Empty);
        }

    }
}
