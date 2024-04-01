using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class PurchaseProduct : ObservableObject
    {
        [ObservableProperty]
        private int _purchaseProductId;

        [ObservableProperty]
        private int _productId;

        [ObservableProperty]
        private Product _product = null!;

        [ObservableProperty]
        private int _purchaseId;

        [ObservableProperty]
        private Purchase _purchase = null!;

        [ObservableProperty]
        private int _count;
    }
}
