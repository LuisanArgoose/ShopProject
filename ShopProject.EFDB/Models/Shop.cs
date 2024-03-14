using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Shop : ObservableObject
{
    [ObservableProperty]
    private int _shopId;

    [ObservableProperty]
	private string _addres = null!;

    [ObservableProperty]
    private int _shopTypeId;

    [ObservableProperty]
    private int _regionId;

    [ObservableProperty]
	private ICollection<Payment> _payments = new List<Payment>();

    [ObservableProperty]
	private ICollection<ProductsInStorage> _productsInStorages = new List<ProductsInStorage>();

    [ObservableProperty]
	private Region _region = null!;

    [ObservableProperty]
	private ICollection<ShopPlan> _shopPlans = new List<ShopPlan>();

    [ObservableProperty]
	private ICollection<ShopPosition> _shopPositions = new List<ShopPosition>();

    [ObservableProperty]
	private ShopType _shopType = null!;
}
