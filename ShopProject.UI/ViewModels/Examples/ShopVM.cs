using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.EFDB.DataModels;
using ShopProject.UI.AuxiliarySystems.AlertSystem;
using ShopProject.UI.Models;
using System.Collections;
using ShopProject.UI.AuxiliarySystems;
using ShopProject.EFDB.Models;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using System.Xml.Serialization;
using System.ComponentModel;
using LiveChartsCore.Defaults;
using static SkiaSharp.HarfBuzz.SKShaper;
using Microsoft.Extensions.Diagnostics.Metrics;


namespace ShopProject.UI.ViewModels.Examples
{
    public partial class ShopVM : ObservableObject
    {

        // Выбранный магазин
        [ObservableProperty]
        private Shop _selectedShop;

        // Поле доступа для для менеджера по продажам
        public bool IsSalesManager { get => Settings.GetActiveUser().Role.IsSalesManager || Settings.GetActiveUser().Role.IsAdmin; }

        private int _shopId;
        public int ShopId
        {
            get => _shopId;
            set
            {
                SetProperty(ref _shopId, value);
                // Вполнение команд загрузки данных

                // Магазин по Id
                GetShopInfoCommand.Execute(this);
                LoadMainPlan();
                LoadMetricsList();
            }
        }
        public ShopVM(int shopId)
        {
            // Стандартный диапозон дат даты

            ShopId = shopId;
        }

        private int _daysInterval = 7;


        [RelayCommand]
        public void SetTimeInterval(string days)
        {
            try
            {
                _daysInterval = int.Parse(days);
                LoadMainPlan();
                LoadMetricsList();

            }
            catch
            {

            }
        }

        private async void LoadMainPlan()
        {
            IsLoading = true;
            await GetMainShopInfo().WaitAsync(CancellationToken.None);
            IsLoading = false;
        }

        private async void LoadMetricsList()
        {
            IsLoading = true;
            await GetMetricsCollection().WaitAsync(CancellationToken.None);
            IsLoading = false;
        }
        /*
        private async void OnSetDates()
        {
            await GetPlanAtributesCollectionCommand.ExecuteAsync(this).WaitAsync(CancellationToken.None);
            SelectedPlanAtribute = PlanAtributesCollection[0];
        }*/

        // отображение процесса загрузки
        [ObservableProperty]
        private bool _isLoading;

        // Загрузка первичных данных магазина с API
        [RelayCommand]
        private async Task GetShopInfo()
        {
            IsLoading = true;
            var response = await ClientDbProvider.GetShopInfo(ShopId).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<Shop>(response, "Данные магазина").WaitAsync(CancellationToken.None);
            if (result != null)
                SelectedShop = result;
            IsLoading = false;
            return;
        }

        // Коллекция для главного плана
        [ObservableProperty]
        private ShopStatsData _shopStatsData = new ShopStatsData();

        [ObservableProperty]
        private List<MetricData> _metricsDataList = new List<MetricData>();

        [ObservableProperty]
        private List<MetricData> _nonPlanedMetricsDataList = new List<MetricData>();
        private async Task GetMainShopInfo()
        {
            // Данные общего плана
            var response = await ClientDbProvider.GetMainShopInfo(ShopId, _daysInterval).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<ShopStatsData>(response, "Загрузка главного плана").WaitAsync(CancellationToken.None);
            if (result == null)
            {
                return;
            }
            MetricsDataList = result.MetricsData.Where(x => x.IsNonPlanedMetric == false).ToList();
            NonPlanedMetricsDataList = result.MetricsData.Where(x => x.IsNonPlanedMetric == true).ToList();

            MainChartClear();
            ShopStatsData = result;
            SetMainChartLabels(ShopStatsData.Day.Select(x => x.ToString("dd.MM.yyyy")).ToList());
            var daysCount = _daysInterval;
            var series = new List<ISeries>();

            if (ShopStatsData.AverageBill.All(x => x != null))
            {
                series.Add(new LineSeries<decimal?>
                {
                    ZIndex = 1,
                    GeometrySize = 0,
                    Fill = null,
                    YToolTipLabelFormatter = point => $"{point.Model}%",
                    Name = "Средний чек",
                    Values = ShopStatsData.AverageBill,
                });
            }
            if (ShopStatsData.RevenueInDay.All(x => x != null))
            {
                series.Add(new LineSeries<decimal?>
                {
                    ZIndex = 1,
                    GeometrySize = 0,
                    Fill = null,
                    YToolTipLabelFormatter = point => $"{point.Model}%",
                    Name = "Выручка",
                    Values = ShopStatsData.RevenueInDay,
                });
            }
            if (ShopStatsData.ProfitInDay.All(x => x != null))
            {
                series.Add(new LineSeries<decimal?>
                {
                    ZIndex = 1,
                    GeometrySize = 0,
                    Fill = null,
                    YToolTipLabelFormatter = point => $"{point.Model}%",
                    Name = "Прибыль",
                    Values = ShopStatsData.ProfitInDay,
                });
            }
            if (ShopStatsData.SalesCountInDay.All(x => x != null))
            {
                series.Add(new LineSeries<decimal?>
                {
                    ZIndex = 1,
                    GeometrySize = 0,
                    Fill = null,
                    YToolTipLabelFormatter = point => $"{point.Model}%",
                    Name = "Количество продаж",
                    Values = ShopStatsData.SalesCountInDay,
                });
            }
            MainSections = new()
            {
                    
                new RectangularSection
                {
                        
                    Label = "План",
                    ZIndex = 10,
                    Yi = 100,
                    Yj = 100,
                    Stroke = new SolidColorPaint
                    {
                        Color = SKColors.Red,
                        StrokeThickness = 3,
                        PathEffect = new DashEffect(new float[] { 6, 6 })
                    }
                },
            };

            MainYAxes =  new()
            {
                new Axis()
                {
                        Labeler = (value) => value.ToString() + "%",
                        LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                        SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                        MinLimit = 0
                }
            };

            SetMainChartCollection(series);
            
            return;
        }
        /*
        private List<PlanAtribute> AddPlanAtributesCollection(List<PlanAtribute> result)
        {
            var range = new List<PlanAtribute>()
            {
                new PlanAtribute()
                {
                    AtributeName = "MainPlan",
                    AtributeViewName = "Общий план"
                }
            };
            range.AddRange(result);
            return range;


        }
        */
        
        [ObservableProperty]
        private List<Metric> _metricsCollection;
        
        [RelayCommand]
        private async Task GetMetricsCollection()
        {
            var response = await ClientDbProvider.GetMetricsCollection().WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<Metric>>(response, "Загрузка коллекции метрик").WaitAsync(CancellationToken.None);
            if (result != null)
            {
                MetricsCollection = result;
                SelectedMetric = MetricsCollection.First();
            }
            
            return;
        }
        
        


        private Metric _selectedMetric;
        
        public Metric SelectedMetric
        {
            get => _selectedMetric;
            set
            {
                SetProperty(ref _selectedMetric, value);
                if (_selectedMetric != null)
                {
                    GetMetricPlanData(_selectedMetric);
                    
                }
                else
                {
                    MetricShopPlansCollection = [];
                    MetricPlanDataCollection = [];
                    MetricChartClear();
                }
                    

            }
        }
        

        [ObservableProperty]
        private List<MetricPlanData> _metricPlanDataCollection;

        
        private async void GetMetricPlanData(Metric metric)
        {
            IsLoading = true;
            await GetMetricShopPlans(metric).WaitAsync(CancellationToken.None);
            var response = await ClientDbProvider.GetMetricPlanDataCollection(ShopId, metric.MetricId, _daysInterval).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<MetricPlanData>> (response, "Загрузка данных метрики: " + metric.MetricViewName).WaitAsync(CancellationToken.None);
            if (result != null)
            {
                MetricChartClear();
                MetricPlanDataCollection = result;
                SetMetricChartLabels(MetricPlanDataCollection.Select(x => x.Day.ToString("dd.MM.yyyy")).ToList());

                var collection = new List<ObservablePoint>();

                if (MetricShopPlansCollection.Count != 0)
                {

                    var sortedCollection = MetricShopPlansCollection.OrderBy(item => item.ShopPlan.SetTime.Date).ToList();
                    collection.AddRange(sortedCollection.Select(x => new ObservablePoint(MetricPlanDataCollection.FindLastIndex(z => z.Day.Date == x.ShopPlan.SetTime.Date), (double?)x.ShopPlan.MetricValue)));
                    
                    var isFirstExist = collection.FirstOrDefault(x => x.X == 0);
                    if (isFirstExist == null)
                        collection.Insert(0, new ObservablePoint(-1, (double?)MetricShopPlansCollection.MinBy(x => x.ShopPlan.SetTime).ShopPlan.MetricValue));
                    collection.Add(new ObservablePoint(MetricPlanDataCollection.Count, (double?)MetricShopPlansCollection.MaxBy(x => x.ShopPlan.SetTime).ShopPlan.MetricValue));
                }
                var series = new List<ISeries>()
                {
                    new StepLineSeries<ObservablePoint>
                    {
                        
                        ZIndex = 999,
                        Name = "План",
                        Values = new List<ObservablePoint>(collection),
                        Stroke = new SolidColorPaint
                        {
                            Color = SKColors.Red,
                            StrokeThickness = 3,
                            PathEffect = new DashEffect(new float[] { 6, 6 })
                        },
                        GeometryStroke = new SolidColorPaint
                        {
                            Color = SKColors.Red,
                        },
                        GeometryFill = new SolidColorPaint
                        {
                            Color = SKColors.Red,
                        },
                        Fill = new SolidColorPaint
                        {
                            Color = new SKColor(255,0,0,25),
                        },
                    },
                    new ColumnSeries<decimal>
                    {
                        
                        ZIndex = 0,
                        Name = metric.MetricViewName,
                        Values = MetricPlanDataCollection.Select(x => x.MetricValue),

                    },
                };

                SetMetricChartCollection(series);

                MetricYAxes = new()
                {
                    new Axis()
                    {
                         
                         LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                         SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                         MinLimit = 0
                    }
                };
            }
            IsLoading = false;
            return;
        }
        

        
        [ObservableProperty]
        private List<ShopPlanModel> _metricShopPlansCollection;

        private async Task GetMetricShopPlans(Metric metric)
        {
            var response = await ClientDbProvider.GetMetricShopPlansCollection(ShopId, metric.MetricId, _daysInterval).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<ShopPlan>>(response, "Загрузка планов метрики: " + metric.MetricName).WaitAsync(CancellationToken.None);
            if (result != null)
            {
                MetricShopPlansCollection = result.OrderBy(x => x.SetTime).Select(x => new ShopPlanModel(x, OnDeleteShopPlan)).ToList();
            }
            return;
        }
        private void OnDeleteShopPlan(object sender, EventArgs e)
        {
            GetMetricPlanData(SelectedMetric);
        }
        
        [ObservableProperty]
        private ShopPlan _addedPlan = new()
        {
            SetTime = DateTime.Today,
            MetricValue = 100
        };

        [RelayCommand]
        private async Task AddShopPlan()
        {
            if (SelectedMetric == null)
                return;
            AddedPlan.MetricId = SelectedMetric.MetricId;
            AddedPlan.ShopId = ShopId;
            await ClientDbProvider.AddShopPlan(AddedPlan).WaitAsync(CancellationToken.None);
            GetMetricPlanData(SelectedMetric);
        } 
        #region MainPlanChart
        [ObservableProperty]
        public List<ISeries> _mainSeries = [];

        [ObservableProperty]
        private List<RectangularSection> _mainSections;

        [ObservableProperty]
        private List<Axis> _mainYAxes;


        private void SetMainChartLabels(List<string> labels)
        {
            MainXAxes[0].Labels = labels;
            OnPropertyChanged(nameof(MainXAxes));
        }

        private void SetMainChartCollection(ISeries seriesCollection)
        {
            MainSeries = new()
            {
                seriesCollection
            };
        }
        private void SetMainChartCollection(List<ISeries> seriesCollection)
        {


            MainSeries = new(seriesCollection);

        }

        private void MainChartClear()
        {
            MainYAxes = [];
            MainSections = [];
        }

        private List<Axis> _mainXAxes = new()
        {
            new Axis
            {

                //LabelsRotation = 45,
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                //ForceStepToMin = true,
                //MinStep = 1
            }
        };
        public List<Axis> MainXAxes
        {
            get => _mainXAxes;
        }
        #endregion



        #region MetricChart
        [ObservableProperty]
        public List<ISeries> _metricSeries = [];

        [ObservableProperty]
        private List<Axis> _metricYAxes;


        private void SetMetricChartLabels(List<string> labels)
        {
            MetricXAxes[0].Labels = labels;
            OnPropertyChanged(nameof(MetricXAxes));
        }

        private void SetMetricChartCollection(ISeries seriesCollection)
        {
            MetricSeries = new()
            {
                seriesCollection
            };
        }
        private void SetMetricChartCollection(List<ISeries> seriesCollection)
        {


            MetricSeries = new(seriesCollection);

        }

        private void MetricChartClear()
        {
            MetricYAxes = [];
        }

        private List<Axis> _metricXAxes = new()
        {
            new Axis
            {

                //LabelsRotation = 45,
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                //ForceStepToMin = true,
                //MinStep = 1
            }
        };
        public List<Axis> MetricXAxes
        {
            get => _metricXAxes;
        }
        #endregion
    }
}
