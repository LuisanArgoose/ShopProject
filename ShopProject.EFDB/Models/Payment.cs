using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ShopId { get; set; }

    public int? ApprovierWorkerId { get; set; }

    public int RecipientWorkerId { get; set; }

    public decimal Amount { get; set; }

    public bool IsApproved { get; set; }

    public string? Comment { get; set; }

    public virtual Worker? ApprovierWorker { get; set; }

    public virtual Worker RecipientWorker { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
