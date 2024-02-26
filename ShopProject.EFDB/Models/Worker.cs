using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Worker
{
    public int WorkerId { get; set; }

    public string Fullname { get; set; } = null!;

    public int WorkerTypeId { get; set; }

    public virtual ICollection<OrderConsignment> OrderConsignments { get; set; } = new List<OrderConsignment>();

    public virtual ICollection<Payment> PaymentApprovierWorkers { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentRecipientWorkers { get; set; } = new List<Payment>();

    public virtual ICollection<ProductConsignment> ProductConsignments { get; set; } = new List<ProductConsignment>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual ICollection<ShopPosition> ShopPositions { get; set; } = new List<ShopPosition>();

    public virtual WorkerType WorkerType { get; set; } = null!;
}
