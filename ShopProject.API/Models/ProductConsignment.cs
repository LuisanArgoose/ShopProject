using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class ProductConsignment
{
    public int ProductConsignmentId { get; set; }

    public int WorkerId { get; set; }

    public DateTime DateTime { get; set; }

    public int OrderConsignmentId { get; set; }

    public virtual OrderConsignment OrderConsignment { get; set; } = null!;

    public virtual ICollection<ProductConsignmentProduct> ProductConsignmentProducts { get; set; } = new List<ProductConsignmentProduct>();

    public virtual Worker Worker { get; set; } = null!;
}
