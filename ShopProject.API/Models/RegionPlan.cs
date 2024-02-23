using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class RegionPlan
{
    public int RegionPlanId { get; set; }

    public int RegionId { get; set; }

    public int Turnover { get; set; }

    public decimal Profit { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Region Region { get; set; } = null!;
}
