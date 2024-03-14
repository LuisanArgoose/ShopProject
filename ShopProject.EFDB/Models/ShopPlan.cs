using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ShopPlan : ObservableObject
{
    [ObservableProperty]
    private int _shopPlanId;

    [ObservableProperty]
    private int _shopId;

    [ObservableProperty]
    private int _turnovet;

    [ObservableProperty]
    private decimal _profit;

    [ObservableProperty]
    private DateOnly _startDate;

    [ObservableProperty]
    private DateOnly _endDate;

    [ObservableProperty]
	private Shop _shop = null!;
}
