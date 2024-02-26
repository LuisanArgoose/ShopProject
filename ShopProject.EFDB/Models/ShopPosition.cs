using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ShopPosition
{
    public int ShopPositionId { get; set; }

    public int ShopId { get; set; }

    public int PositionId { get; set; }

    public int? WorkerId { get; set; }

    public decimal? Salary { get; set; }

    public virtual Position Position { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;

    public virtual Worker? Worker { get; set; }
}
