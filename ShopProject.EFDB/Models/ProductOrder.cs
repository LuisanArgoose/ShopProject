using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ProductOrder : ObservableObject
{
    [ObservableProperty]
    private int _productOrderId;

    [ObservableProperty]
    private DateTime _dateTime;

    [ObservableProperty]
    private int _workerId;

    [ObservableProperty]
    private int _orderConsignmentId;

    [ObservableProperty]
    private bool? _isApproved;

    [ObservableProperty]
	private OrderConsignment _orderConsignment = null!;

    [ObservableProperty]
	private ICollection<ProductOrderProductInStorage> _productOrderProductInStorages = new List<ProductOrderProductInStorage>();

    [ObservableProperty]
	private Worker _worker = null!;
}
