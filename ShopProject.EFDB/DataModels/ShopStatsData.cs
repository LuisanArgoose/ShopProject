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
        private List<decimal?> _salesCountInDay = new();
        [ObservableProperty]
        private List<decimal?> _averageBill = new();
        [ObservableProperty]
        private List<decimal?> _revenueInDay = new();
        [ObservableProperty]
        private List<decimal?> _profitInDay = new();
        

        [ObservableProperty]
        private List<MetricData> _metricsData = new();

    }
}
