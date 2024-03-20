using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductOrderProductInStorage : ObservableObject
{
    [ObservableProperty]
    private int _productOrderId;

    [ObservableProperty]
    private int _productInStorageId;

    [ObservableProperty]
    private int _productCount;

    [ObservableProperty]
	private ProductsInStorage _productInStorage = null!;

    [ObservableProperty]
	private ProductOrder _productOrder = null!;
}
