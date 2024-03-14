using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class WorkerType : ObservableObject
{
    [ObservableProperty]
    private int _workerTypeId;

    [ObservableProperty]
    private string _workerTypeName = null!;

    [ObservableProperty]
    private ICollection<Worker> _workers  = new List<Worker>();
}
