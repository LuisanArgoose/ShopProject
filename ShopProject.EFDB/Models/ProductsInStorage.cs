using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductsInStorage : ObservableObject
{
    [ObservableProperty]
    private int _productInStorageId;

    [ObservableProperty]
    private int _shopId;

    [ObservableProperty]
    private int _productId;

    [ObservableProperty]
    private int _productCount;

    [ObservableProperty]
	private Product _product = null!;

    [ObservableProperty]
	private ICollection<ProductOrderProductInStorage> _productOrderProductInStorages = new List<ProductOrderProductInStorage>();

    [ObservableProperty]
	private ICollection<PurchaseProductInStorage> _purchaseProductInStorages = new List<PurchaseProductInStorage>();

    [ObservableProperty]
	private ICollection<RefundProductInStorage> _refundProductInStorages = new List<RefundProductInStorage>();

    [ObservableProperty]
	private Shop _shop = null!;
}
