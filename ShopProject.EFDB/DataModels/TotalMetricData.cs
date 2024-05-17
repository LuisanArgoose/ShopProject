using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class TotalMetricData : ObservableObject
    {
        public TotalMetricData(string metricName)
        {
            MetricName = metricName;
        }
        [ObservableProperty]
        private string _metricName;
        [ObservableProperty]
        private decimal _totalMetricValue;
        [ObservableProperty]
        private List<ShopMetric> _shopMetricList = new();
    }
}
