using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class ShopPosition : ObservableObject
{
    [ObservableProperty]
    private int _shopPositionId;

    [ObservableProperty]
    private int _shopId;

    [ObservableProperty]
    private int _positionId;

    [ObservableProperty]
    private int? _workerId;

    [ObservableProperty]
    private decimal? _salary;

    [ObservableProperty]
	private Position _position = null!;

    [ObservableProperty]
	private Shop _shop = null!;

    [ObservableProperty]
    private Worker? _worker;
}
