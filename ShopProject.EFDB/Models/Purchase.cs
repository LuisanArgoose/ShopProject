using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Purchase : ObservableObject
{
    [ObservableProperty]
    private int _purchaseId;

    [ObservableProperty]
    private DateTime _dateTime;

    [ObservableProperty]
    private int _workerId;

    [ObservableProperty]
	private ICollection<PurchaseProductInStorage> _purchaseProductInStorages = new List<PurchaseProductInStorage>();

    [ObservableProperty]
	private ICollection<Refund> _refunds = new List<Refund>();

    [ObservableProperty]
	private Worker _worker = null!;
}
