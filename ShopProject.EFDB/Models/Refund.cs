using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Refund
{
    public int RefundId { get; set; }

    public DateTimeOffset DateTime { get; set; }

    public int WorkerId { get; set; }

    public int PurchaseId { get; set; }

    public virtual Purchase Purchase { get; set; } = null!;

    public virtual ICollection<RefundProductInStorage> RefundProductInStorages { get; set; } = new List<RefundProductInStorage>();

    public virtual Worker Worker { get; set; } = null!;
}
