using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private int _productId;

        [ObservableProperty]
        private string _productName = null!;

        [ObservableProperty]
        private decimal _costPrice;

        [ObservableProperty]
        private decimal _sellPrice;

        [ObservableProperty]
        private ICollection<ProductPlan> _productPlans = new List<ProductPlan>();

        [ObservableProperty]
        private ICollection<PurchaseProduct> _purchaseProducts = new List<PurchaseProduct>();
    }
}
