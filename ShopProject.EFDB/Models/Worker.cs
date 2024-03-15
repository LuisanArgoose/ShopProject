using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Worker : ObservableObject
{
    [ObservableProperty]
    private int _workerId;

    [ObservableProperty]
    private string _fullname = null!;

    [ObservableProperty]
    private string _login = null!;

    [ObservableProperty]
    private string _password = null!;

    [ObservableProperty]
    private int _workerTypeId;

    [ObservableProperty]
    private ICollection<OrderConsignment> _orderConsignments = new List<OrderConsignment>();

    [ObservableProperty]
    private ICollection<Payment> _paymentApprovierWorkers = new List<Payment>();

    [ObservableProperty]
	private ICollection<Payment> _paymentRecipientWorkers = new List<Payment>();

    [ObservableProperty]
	private ICollection<ProductConsignment> _productConsignments = new List<ProductConsignment>();

    [ObservableProperty]
	private ICollection<ProductOrder> _productOrders = new List<ProductOrder>();

    [ObservableProperty]
	private ICollection<Purchase> _purchases = new List<Purchase>();

    [ObservableProperty]
	private ICollection<Refund> _refunds = new List<Refund>();

    [ObservableProperty]
	private ICollection<ShopPosition> _shopPositions = new List<ShopPosition>();

    [ObservableProperty]
	private WorkerType _workerType = null!;
}
