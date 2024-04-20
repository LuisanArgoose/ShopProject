using ShopProject.EFDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.DataModels
{
    public partial class ShopStats : ObservableObject
    {
        [ObservableProperty]
        private DateTime _day;
        [ObservableProperty]
        private decimal _averageBill;
        [ObservableProperty]
        private decimal _allProfit;
        [ObservableProperty]
        private decimal _clearProfit;
        [ObservableProperty]
        private int _purchasesCount;
    }
}
