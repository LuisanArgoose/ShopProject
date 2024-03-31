using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class Role : ObservableObject
{
    [ObservableProperty]
    private int _roleId;

    [ObservableProperty]
    private string _roleName = null!;

    [ObservableProperty]
    private bool _isShopManager;

    [ObservableProperty]
    private bool _isSalesManager;

    [ObservableProperty]
    private bool _isAdmin;

    [ObservableProperty]
    private ICollection<User> _users  = new List<User>();
}
