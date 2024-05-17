using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using ShopProject.EFDB.DataModels;
using ShopProject.EFDB.Migrations;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models
{
    public partial class PieChartModel : ObservableObject
    {
        [ObservableProperty]
        private TotalMetricData _totalMetricData;
        public PieChartModel(TotalMetricData totalMetricData)
        {
            TotalMetricData = totalMetricData;
            Init();


        }
        [RelayCommand]
        public void Init()
        {
            MetricSeries = new List<ISeries>();

            foreach (var metric in TotalMetricData.ShopMetricList)
            {
                var series = new PieSeries<decimal>
                {
                    Values = new ObservableCollection<decimal> { metric.MetricValue },
                    Name = metric.ShopName,
                    DataLabelsPaint = new SolidColorPaint(SKColors.White)
                    {
                        SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
                    },
                    ToolTipLabelFormatter = point =>
                    {
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;
                        return $"{pv}{Environment.NewLine}{sv.Share:P2}";
                    },
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                    DataLabelsFormatter = point =>
                    {
                        return metric.ShopName; // Используем имя метрики как метку (label)
                    }
                };

                MetricSeries.Add(series);
            }


            /*
            var data = TotalMetricData.ShopMetricList.Select(x => x.MetricValue);
            MetricSeries = data.AsPieSeries((value, series) =>
            {

                series.DataLabelsPaint = new SolidColorPaint(SKColors.White)
                {
                    SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
                };
                series.ToolTipLabelFormatter =
                    point =>
                    {
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;

                        var a = $"{pv}{Environment.NewLine}{sv.Share:P2}";
                        return a;
                    };
                
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;

                series.DataLabelsFormatter =
                    point =>
                    {
                        
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;
                        
                        var a = $"{value}";
                        return a;
                    };
            });*/

        }

        [ObservableProperty]
        public List<ISeries> _metricSeries = [];


    }
}
