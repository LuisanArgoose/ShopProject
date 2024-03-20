using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class OrderConsignment : ObservableObject
{
    [ObservableProperty]
    private int _orderConsignmentId;

    [ObservableProperty]
    private int _workerId;

    [ObservableProperty]
    private DateTime _dateTime;

    [ObservableProperty]
	private ICollection<ProductConsignment> _productConsignments = new List<ProductConsignment>();

    [ObservableProperty]
	private ICollection<ProductOrder> _productOrders = new List<ProductOrder>();

    [ObservableProperty]
	private Worker _worker = null!;
}
