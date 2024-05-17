using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models
{

    public partial class TotalPlanModel : ObservableValue
    {
        private readonly Random _r = new();
        public SolidColorPaint Paint { get; set; }
        public string ShopName { get; set; }
        public TotalPlanModel(string shopName, decimal? value)
        {
            ShopName = shopName;
            Value = (double?)value;
            Paint = new SolidColorPaint(ColorPalletes.MaterialDesign500[_r.Next(0,9)].AsSKColor());
        }
    }
}
