using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class WorkerPlan : ObservableObject
    {
        [ObservableProperty]
        private int _workerPlanId;

        [ObservableProperty]
        private int _purchasesCount;

        [ObservableProperty]
        private int _productsCount;

        [ObservableProperty]
        private decimal _averageBill;

        [ObservableProperty]
        private DateTime _updatedTime; 

        [ObservableProperty]
        private int _shopId;

        [ObservableProperty]
        private Shop _shop = null!;
    }
}
