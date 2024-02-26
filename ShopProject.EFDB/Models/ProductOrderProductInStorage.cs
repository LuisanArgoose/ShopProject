using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductOrderProductInStorage
{
    public int ProductOrderId { get; set; }

    public int ProductInStorageId { get; set; }

    public int ProductCount { get; set; }

    public virtual ProductsInStorage ProductInStorage { get; set; } = null!;

    public virtual ProductOrder ProductOrder { get; set; } = null!;
}
