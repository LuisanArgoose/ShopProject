using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string Addres { get; set; } = null!;

    public int ShopTypeId { get; set; }

    public int RegionId { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ProductsInStorage> ProductsInStorages { get; set; } = new List<ProductsInStorage>();

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<ShopPlan> ShopPlans { get; set; } = new List<ShopPlan>();

    public virtual ICollection<ShopPosition> ShopPositions { get; set; } = new List<ShopPosition>();

    public virtual ShopType ShopType { get; set; } = null!;
}
