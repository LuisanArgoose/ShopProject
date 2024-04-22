using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class PlanAtribute : ObservableObject
    {
        [ObservableProperty]
        private int _planAtributeId;

        [ObservableProperty]
        private string _atributeName = null!;
        [ObservableProperty]
        private string _atributeViewName = null!;

        [ObservableProperty]
        private ICollection<ShopPlan> _shopPlans = new List<ShopPlan>();

    }
}
