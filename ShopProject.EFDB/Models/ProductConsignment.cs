using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductConsignment : ObservableObject
{
    [ObservableProperty]
    private int _productConsignmentId;

    [ObservableProperty]
    private int _workerId;

    [ObservableProperty]
    private DateTime _dateTime;

    [ObservableProperty]
    private int _orderConsignmentId;

    [ObservableProperty]
	private OrderConsignment _orderConsignment = null!;

    [ObservableProperty]
	private ICollection<ProductConsignmentProduct> _productConsignmentProducts = new List<ProductConsignmentProduct>();

    [ObservableProperty]
	private Worker _worker = null!;
}
