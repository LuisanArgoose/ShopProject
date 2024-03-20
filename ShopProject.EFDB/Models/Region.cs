using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Region : ObservableObject
{
    [ObservableProperty]
    private int _regionId;

    [ObservableProperty]
	private  string _regionName = null!;

    [ObservableProperty]
	private ICollection<RegionPlan> _regionPlans = new List<RegionPlan>();

    [ObservableProperty]
	private ICollection<Shop> _shops = new List<Shop>();
}
