﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models
{
    public partial class Cashier : ObservableObject
    {
        [ObservableProperty]
        private int _cashierId;

        [ObservableProperty]
        private string _fullName = null!;

        [ObservableProperty]
        private ICollection<Shop> _shops = new List<Shop>();

        [ObservableProperty]
        private ICollection<Purchase> _purchases = new List<Purchase>();
    }
}
