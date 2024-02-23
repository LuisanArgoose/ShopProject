using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class OrderConsignment
{
    public int OrderConsignmentId { get; set; }

    public int WorkerId { get; set; }

    public DateTime DateTime { get; set; }

    public virtual ICollection<ProductConsignment> ProductConsignments { get; set; } = new List<ProductConsignment>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual Worker Worker { get; set; } = null!;
}
