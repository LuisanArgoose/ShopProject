using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class SalaryType : ObservableObject
{
    [ObservableProperty]
    private int _salaryTypeId;

    [ObservableProperty]
	private string _salaryTypeName = null!;

    [ObservableProperty]
	private ICollection<Position> _positions = new List<Position>();
}
