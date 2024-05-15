using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models
{
    public partial class PieChartModel : ObservableObject
    {
        public PieChartModel()
        {
            var outer = 0;
            var data = new[] { 6, 5, 4, 3 };
           
        }
    

        [ObservableProperty]
        private string _metricName;
        [ObservableProperty]
        public IEnumerable<ISeries> _metricSeries = [];

        
    }
}
