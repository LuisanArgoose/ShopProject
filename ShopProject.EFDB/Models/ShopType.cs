using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ShopType : ObservableObject
{
    [ObservableProperty]
    private int _shopTypeId;

    [ObservableProperty]
	private string _shopTypeName = null!;

    [ObservableProperty]
	private ICollection<Shop> _shops = new List<Shop>();
}
