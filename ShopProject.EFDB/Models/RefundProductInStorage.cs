using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class RefundProductInStorage
{
    public int RefundId { get; set; }

    public int ProductInStorageId { get; set; }

    public int ProductCount { get; set; }

    public virtual ProductsInStorage ProductInStorage { get; set; } = null!;

    public virtual Refund Refund { get; set; } = null!;
}
