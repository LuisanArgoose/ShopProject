using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class Purchase : ObservableObject
    {
        [ObservableProperty]
        private int _purchaseId;

        [ObservableProperty]
        private int _cashierId;

        [ObservableProperty]
        private Cashier _cashier = null!;

        [ObservableProperty]
        private ICollection<PurchaseProduct> _purchaseProducts = new List<PurchaseProduct>();

    }
}
