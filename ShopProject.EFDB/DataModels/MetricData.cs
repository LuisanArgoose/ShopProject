using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class MetricData : ObservableObject
    {

        public MetricData(string metricName)
        {
            MetricName = metricName;
            MetricOldValue = 0;
            MetricValue = 0;
            MetricPlanResult = 0;
        }

        [ObservableProperty]
        private string _metricName;

        [ObservableProperty]
        private decimal? _metricValue;

        [ObservableProperty]
        private decimal? _metricOldValue;

        [ObservableProperty]
        private decimal? _metricPlanResult;


    }
}
