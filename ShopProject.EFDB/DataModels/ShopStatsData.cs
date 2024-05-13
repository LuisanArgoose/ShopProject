using ShopProject.EFDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class ShopStatsData : ObservableObject
    {
        [ObservableProperty]
        private List<DateTime> _day = new();
        [ObservableProperty]
        private List<decimal?> _salesCount = new();
        [ObservableProperty]
        private List<decimal?> _averageBill = new();
        [ObservableProperty]
        private List<decimal?> _revenue = new();
        [ObservableProperty]
        private List<decimal?> _profit = new();
        

        [ObservableProperty]
        private List<MetricData> _metricsData = new();

    }
}
