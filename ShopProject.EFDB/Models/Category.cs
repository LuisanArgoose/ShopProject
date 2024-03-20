using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Category : ObservableObject
{
    [ObservableProperty]
    private int _categoryId;

    [ObservableProperty]
    private string _categoryName  = null!;

    [ObservableProperty]
	private ICollection<Product> _products  = new List<Product>();
}
