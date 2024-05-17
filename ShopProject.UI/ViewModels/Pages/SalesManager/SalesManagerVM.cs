using LiveChartsCore;
using LiveChartsCore.ConditionalDraw;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
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
                foreach(var series in result)
                {
                    data.Add(new RowSeries<TotalPlanModel>()
                    {
                        Values = new List<TotalPlanModel>(){ new TotalPlanModel(series.ShopName, series.MetricsData.Select(x => x.MetricPlanResult + 100).Sum()) },
                        DataLabelsPosition = DataLabelsPosition.End,
                        DataLabelsTranslate = new(-1, 0),
                        DataLabelsFormatter = point => $"{point.Model!.ShopName} {point.Coordinate.PrimaryValue}%",
                        TooltipLabelFormatter = point => $"{point.PrimaryValue}%",
                        Name = series.ShopName,
                        MaxBarWidth = 50,
                        Padding = 10,

                    });
                }


                ShopSeries = data;
            }
            
            return;
        }

        [ObservableProperty]
        private List<Axis> _shopXAxes = new()
        {
            new Axis
            {

                MinLimit = 0,

            }
        };

        [ObservableProperty]
        private ObservableCollection<ISeries> _shopSeries;



    }
}
