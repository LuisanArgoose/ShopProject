using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Models;

public partial class Shop : ObservableObject
{
    [ObservableProperty]
    private int _shopId;

    [ObservableProperty]
    private string _address = null!;

    [ObservableProperty]
    private int _userId;

    [ObservableProperty]
    private User _user = null!;

}

