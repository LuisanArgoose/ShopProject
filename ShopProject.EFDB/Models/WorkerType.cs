using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class WorkerType
{
    public int WorkerTypeId { get; set; }

    public string WorkerTypeName { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
