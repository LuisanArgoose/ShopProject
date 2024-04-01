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
    private string _shopName = null!;

    [ObservableProperty]
    private string _address = null!;

    [ObservableProperty]
    private ICollection<User> _users = new List<User>();

    [ObservableProperty]
    private ICollection<Cashier> _cashiers = new List<Cashier>();

    [ObservableProperty]
    private ICollection<ProductPlan> _productPlans = new List<ProductPlan>();

    [ObservableProperty]
    private ICollection<ShopPlan> _shopPlans = new List<ShopPlan>();

    [ObservableProperty]
    private ICollection<WorkerPlan> _workerPlans = new List<WorkerPlan>();
}

