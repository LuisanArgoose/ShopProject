﻿using System;
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
        private int _metricId;

        [ObservableProperty]
        private Metric _metric = null!;

        [ObservableProperty]
        private decimal _metricValue;

        [ObservableProperty]
        private DateTime _setTime;

        [ObservableProperty]
        private int _shopId;

        [ObservableProperty]
        private Shop _shop = null!;
    }
}
