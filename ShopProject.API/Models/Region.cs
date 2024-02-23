using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string RegionName { get; set; } = null!;

    public virtual ICollection<RegionPlan> RegionPlans { get; set; } = new List<RegionPlan>();

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
