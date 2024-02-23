using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public DateTime DateTime { get; set; }

    public int WorkerId { get; set; }

    public virtual ICollection<PurchaseProductInStorage> PurchaseProductInStorages { get; set; } = new List<PurchaseProductInStorage>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual Worker Worker { get; set; } = null!;
}
