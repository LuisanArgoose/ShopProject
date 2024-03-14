using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class PurchaseProductInStorage : ObservableObject
{
    [ObservableProperty]
    private int _purchaseId;

    [ObservableProperty]
    private int _productsInStorageId;

    [ObservableProperty]
    private int _productCount;

    [ObservableProperty]
	private ProductsInStorage _productsInStorage = null!;

    [ObservableProperty]
	private Purchase _purchase = null!;
}
