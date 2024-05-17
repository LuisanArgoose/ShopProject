using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class ShopMetric : ObservableObject
    {
        [ObservableProperty]
        private string _shopName;
        [ObservableProperty]
        private decimal _metricValue;
    }
}
