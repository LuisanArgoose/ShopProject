using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Product : ObservableObject
{
    [ObservableProperty]
    private int _productId;

    [ObservableProperty]
    private int _categoryId;

    [ObservableProperty]
	private string _productName = null!;

    [ObservableProperty]
	private string _code = null!;

    [ObservableProperty]
    private decimal _buyCost;

    [ObservableProperty]
    private decimal _sellCost;

    [ObservableProperty]
	private string _barcode = null!;

    [ObservableProperty]
	private Category _category = null!;

    [ObservableProperty]
	private ICollection<ProductConsignmentProduct> _productConsignmentProducts = new List<ProductConsignmentProduct>();

    [ObservableProperty]
	private ICollection<ProductsInStorage> _productsInStorages = new List<ProductsInStorage>();
}
