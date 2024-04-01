using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class User : ObservableObject
{
    [ObservableProperty]
    private int _userId;

    [ObservableProperty]
    private string _fullname = null!;

    [ObservableProperty]
    private string _login = null!;

    [ObservableProperty]
    private string _password = null!;

    [ObservableProperty]
    private int _roleId;

    [ObservableProperty]
    private Role _role = null!;

    [ObservableProperty]
    private ICollection<Shop> _shops = new List<Shop>();

}
