using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class RefundProductInStorage : ObservableObject
{
    [ObservableProperty]
    private int _refundId;

    [ObservableProperty]
    private int _productInStorageId;

    [ObservableProperty]
    private int _productCount;

    [ObservableProperty]
	private ProductsInStorage _productInStorage = null!;

    [ObservableProperty]
	private Refund _refund = null!;
}
