using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class ProductsInStorage
{
    public int ProductInStorageId { get; set; }

    public int ShopId { get; set; }

    public int ProductId { get; set; }

    public int ProductCount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductOrderProductInStorage> ProductOrderProductInStorages { get; set; } = new List<ProductOrderProductInStorage>();

    public virtual ICollection<PurchaseProductInStorage> PurchaseProductInStorages { get; set; } = new List<PurchaseProductInStorage>();

    public virtual ICollection<RefundProductInStorage> RefundProductInStorages { get; set; } = new List<RefundProductInStorage>();

    public virtual Shop Shop { get; set; } = null!;
}
