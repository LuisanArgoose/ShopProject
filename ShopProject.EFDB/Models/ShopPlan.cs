using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class ShopPlan : ObservableObject
    {
        [ObservableProperty]
        private int _shopPlanId;

        [ObservableProperty]
        private int _tradeTurnover;

        [ObservableProperty]
        private int _moneyTurnover;

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
