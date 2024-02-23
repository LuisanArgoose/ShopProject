using System;
using System.Collections.Generic;

namespace ShopProject.API.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public decimal BuyCost { get; set; }

    public decimal SellCost { get; set; }

    public string Barcode { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ProductConsignmentProduct> ProductConsignmentProducts { get; set; } = new List<ProductConsignmentProduct>();

    public virtual ICollection<ProductsInStorage> ProductsInStorages { get; set; } = new List<ProductsInStorage>();
}
