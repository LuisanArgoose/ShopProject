using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class SalaryType
{
    public int SalaryTypeId { get; set; }

    public string SalaryTypeName { get; set; } = null!;

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
