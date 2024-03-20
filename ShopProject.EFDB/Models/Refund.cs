using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Refund : ObservableObject
{
    [ObservableProperty]
    private int _refundId;

    [ObservableProperty]
    private DateTimeOffset _dateTime;

    [ObservableProperty]
    private int _workerId;

    [ObservableProperty]
    private int _purchaseId;

    [ObservableProperty]
	private Purchase _purchase = null!;

    [ObservableProperty]
	private ICollection<RefundProductInStorage> _refundProductInStorages = new List<RefundProductInStorage>();

    [ObservableProperty]
	private Worker _worker = null!;
}
