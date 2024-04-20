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
        private int _planAtributeId;

        [ObservableProperty]
        private PlanAtribute _planAtribute = null!;

        [ObservableProperty]
        private decimal _atributeValue;

        [ObservableProperty]
        private DateTime _setTime;

        [ObservableProperty]
        private int _shopId;

        [ObservableProperty]
        private Shop _shop = null!;
    }
}
