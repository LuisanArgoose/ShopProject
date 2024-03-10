﻿using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class TestTable
{
    public int TestId { get; set; }

    public string TestText { get; set; } = null!;

    public string? TextToUpdate { get; set; }
    public override string ToString()
    {
        return TestId.ToString() + TestText + TextToUpdate;
    }
}
