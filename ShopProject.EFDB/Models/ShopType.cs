using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ShopType
{
    public int ShopTypeId { get; set; }

    public string ShopTypeName { get; set; } = null!;

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
