using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductConsignmentProduct
{
    public int ProductId { get; set; }

    public int ProductConsignmentId { get; set; }

    public int Count { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductConsignment ProductConsignment { get; set; } = null!;
}
