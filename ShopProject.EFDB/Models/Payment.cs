using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Payment : ObservableObject
{
    [ObservableProperty]
    private int _paymentId;

    [ObservableProperty]
    private int _shopId;

    [ObservableProperty]
    private int? _approvierWorkerId;

    [ObservableProperty]
    private int _recipientWorkerId;

    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private bool _isApproved;

    [ObservableProperty]
    private string? _comment;

    [ObservableProperty]
    private Worker? _approvierWorker;

    [ObservableProperty]
	private Worker _recipientWorker = null!;

    [ObservableProperty]
	private Shop _shop = null!;
}
