using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductConsignmentProduct : ObservableObject
{
    [ObservableProperty]
    private int _productId;

    [ObservableProperty]
    private int _productConsignmentId;

    [ObservableProperty]
    private int _count;

    [ObservableProperty]
	private Product _product = null!;

    [ObservableProperty]
	private ProductConsignment _productConsignment = null!;
}
