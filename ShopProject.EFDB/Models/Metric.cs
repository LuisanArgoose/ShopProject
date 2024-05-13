using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class Metric : ObservableObject
    {
        [ObservableProperty]
        private int _MetricId;

        [ObservableProperty]
        private string _MetricName = null!;
        [ObservableProperty]
        private string _MetricViewName = null!;

        [ObservableProperty]
        private ICollection<ShopPlan> _shopPlans = new List<ShopPlan>();

    }
}
