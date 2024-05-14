using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class MetricPlanData : ObservableObject
    {
        [ObservableProperty]
        private DateTime _day;

        [ObservableProperty]
        private decimal _metricValue;
    }
}
