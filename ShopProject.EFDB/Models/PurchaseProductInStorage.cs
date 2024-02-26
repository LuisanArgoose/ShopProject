using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class PurchaseProductInStorage
{
    public int PurchaseId { get; set; }

    public int ProductsInStorageId { get; set; }

    public int ProductCount { get; set; }

    public virtual ProductsInStorage ProductsInStorage { get; set; } = null!;

    public virtual Purchase Purchase { get; set; } = null!;
}
