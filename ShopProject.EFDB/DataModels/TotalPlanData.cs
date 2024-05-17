﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class TotalPlanData : ObservableObject
    {
        [ObservableProperty]
        private string _shopName;
        [ObservableProperty]
        private List<MetricData> _metricsData = new();
    }
}
