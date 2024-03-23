using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Role : ObservableObject
{
    [ObservableProperty]
    private int _roleId;

    [ObservableProperty]
	private  string _roleName = null!;

    [ObservableProperty]
    private bool _isManager;

    [ObservableProperty]
    private bool _isRegionManager;

    [ObservableProperty]
    private bool _isAdmin;

    [ObservableProperty]
	private ICollection<Position> _positions = new List<Position>();
}
