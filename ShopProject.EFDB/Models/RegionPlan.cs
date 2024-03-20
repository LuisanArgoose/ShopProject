using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class RegionPlan : ObservableObject
{
    [ObservableProperty]
    private int _regionPlanId;

    [ObservableProperty]
    private int _regionId;

    [ObservableProperty]
    private int _turnover;

    [ObservableProperty]
    private decimal _profit;

    [ObservableProperty]
    private DateOnly _startDate;

    [ObservableProperty]
    private DateOnly _endDate;

    [ObservableProperty]
	private Region _region = null!;
}
