using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductOrder
{
    public int ProductOrderId { get; set; }

    public DateTime DateTime { get; set; }

    public int WorkerId { get; set; }

    public int OrderConsignmentId { get; set; }

    public bool? IsApproved { get; set; }

    public virtual OrderConsignment OrderConsignment { get; set; } = null!;

    public virtual ICollection<ProductOrderProductInStorage> ProductOrderProductInStorages { get; set; } = new List<ProductOrderProductInStorage>();

    public virtual Worker Worker { get; set; } = null!;
}
