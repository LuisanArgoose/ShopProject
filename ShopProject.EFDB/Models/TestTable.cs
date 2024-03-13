using System;
using System.Collections.Generic;

namespace ShopProject.EFDB.Models;

public partial class TestTable : ObservableObject
{
    [ObservableProperty]
    private int _testId;

    [ObservableProperty]
    private string _testText = null!;

    [ObservableProperty]
    private string? _textToUpdate;

    public override string ToString()
    {
        return TestId.ToString() + TestText + TextToUpdate;
    }
}
