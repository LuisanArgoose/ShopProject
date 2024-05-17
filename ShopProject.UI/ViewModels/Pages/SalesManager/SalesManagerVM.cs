using LiveChartsCore;
using LiveChartsCore.ConditionalDraw;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Microsoft.VisualBasic;
using ShopProject.EFDB.DataModels;
using ShopProject.EFDB.Models;
using ShopProject.UI.AuxiliarySystems;
using ShopProject.UI.Models;
using ShopProject.UI.Models.SettingsComponents.APISettings;
using ShopProject.UI.ViewModels.Examples;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages.SalesManager
{
    public partial class SalesManagerVM : ObservableObject
    {
        public SalesManagerVM()
        {
            LoadTotalMetricData();
            LoadTotalPlanData();
        }
        [ObservableProperty]
        private List<PieChartModel> _pieChartModelList;
        private int _daysInterval = 7;


        [RelayCommand]
        public void SetTimeInterval(string days)
        {
            try
            {
                _daysInterval = int.Parse(days);
                LoadTotalMetricData();
                LoadTotalPlanData();

            }
            catch
            {

            }


        }

        private async void LoadTotalMetricData()
        {

            await GetTotalMetricData();

        }

        private async Task GetTotalMetricData()
        {
            var response = await ClientDbProvider.GetTotalMetricData(_daysInterval).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<TotalMetricData>>(response, "Общий план всех магазинов").WaitAsync(CancellationToken.None);
            if (result != null)
                PieChartModelList = new(result.Select(x => new PieChartModel(x)));
            return;
        }

        private async void LoadTotalPlanData()
        {
            await GetTotalPlanData();
        }
        private async Task GetTotalPlanData()
        {
            var response = await ClientDbProvider.GetTotalPlanData(_daysInterval).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<TotalPlanData>>(response, "Успеваемость всех магазинов").WaitAsync(CancellationToken.None);
            if (result != null)
            {

                var data = new ObservableCollection<ISeries>();

                var metricNames = result.First().MetricsData.Select(x => x.MetricName) ;
                foreach(var metricName in metricNames)
                {
                    data.Add(new StackedRowSeries<decimal?>()
                    {
                        Values = result.Select(x => x.MetricsData.First(z => z.MetricName == metricName).MetricPlanResult + 100 ),
                        DataLabelsPosition = DataLabelsPosition.End,
                        DataLabelsTranslate = new(-1, 0),
                        
                        //DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue}",
                        XToolTipLabelFormatter = point => $"{point.Label}",
                        MaxBarWidth = 50,
                        Padding = 10,
                        Name = metricName,
                    });

                }
                
               

                ShopYAxes = new()
                {
                    new Axis
                    {
                        //Labels = new List<string>() {"Магазин 1"}
                        Labels = result.Select(x => x.ShopName).ToList(),
                        MinStep = 1,
                        LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),

                    }
                };

                ShopSeries = new(data);
                
            }
            
            return;
        }

        [ObservableProperty]
        private List<Axis> _shopYAxes;

        [ObservableProperty]
        private List<Axis> _shopXAxes = new()
        {
            new Axis
            {
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                MinStep = 100,
                MinLimit = 0,
                Labeler = (value) =>
                {
                    return $"{value}%";
                }

            }
        };

        [ObservableProperty]
        private ObservableCollection<ISeries> _shopSeries;



    }
}
