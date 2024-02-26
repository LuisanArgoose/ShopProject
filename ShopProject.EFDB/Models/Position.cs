using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public int SalaryTypeId { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual SalaryType SalaryType { get; set; } = null!;

    public virtual ICollection<ShopPosition> ShopPositions { get; set; } = new List<ShopPosition>();
}
