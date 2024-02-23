using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class ShopPlan
{
    public int ShopPlanId { get; set; }

    public int ShopId { get; set; }

    public int Turnovet { get; set; }

    public decimal Profit { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}
