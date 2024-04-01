using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class ProductPlan : ObservableObject
    {
        [ObservableProperty]
        private int _productPlanId;

        [ObservableProperty]
        private int _productId;

        [ObservableProperty]
        private int _expectedQuantity;

        [ObservableProperty]
        private DateTime _updatedTime;

        [ObservableProperty]
        private Product _product = null!;

        [ObservableProperty]
        private int _shopId;

        [ObservableProperty]
        private Shop _shop = null!;
    }
}
