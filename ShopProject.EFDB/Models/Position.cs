using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Position : ObservableObject
{
    [ObservableProperty]
    private int _positionId;

    [ObservableProperty]
	private  string _positionName  = null!;

    [ObservableProperty]
    private int _salaryTypeId;

    [ObservableProperty]
    private int? _roleId;

    [ObservableProperty]
    private Role? _role;

    [ObservableProperty]
	private SalaryType _salaryType  = null!;

    [ObservableProperty]
	private ICollection<ShopPosition> _shopPositions  = new List<ShopPosition>();
}
