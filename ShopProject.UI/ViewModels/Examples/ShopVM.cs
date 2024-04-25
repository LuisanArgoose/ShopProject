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
using ShopProject.UI.Models.Examples;
using System.Collections;
using ShopProject.UI.AuxiliarySystems;
using ShopProject.EFDB.Models;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using System.Xml.Serialization;
using System.ComponentModel;
using LiveChartsCore.Defaults;


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

                // Общий план магазина
                GetMainShopPlanCommand.Execute(this);
            }
        }
        public ShopVM(int shopId)
        {
            // Инициализация команд
            GetMainShopPlanCommand = new AsyncRelayCommand(GetMainShopPlan);
            GetShopInfoCommand = new AsyncRelayCommand(GetShopInfo);
            // Стандартный диапозон дат даты
        
            ShopId = shopId;
        }



        private DateTime _startDate = DateTime.Today.AddDays(-15);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                SetProperty(ref _startDate, value);
                OnSetDates();
            }
        }

        private DateTime _endDate = DateTime.Today;

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                SetProperty(ref _endDate, value);
                OnSetDates();
            }
        }


        private async void OnSetDates()
        {
            await GetMainShopPlanCommand.ExecuteAsync(this);
        }
        [ObservableProperty]
        private bool _isLoading;

        //OnPropertyChanged(nameof(Series));
        //OnPropertyChanged(nameof(XAxes));
        public IAsyncRelayCommand GetShopInfoCommand { get; }

        // Загрузка данных магазина с API
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

        //Команда получения данных общего плана
        public IAsyncRelayCommand GetMainShopPlanCommand { get; }

        // Коллекция для главного плана
        [ObservableProperty]
        private ShopStatsData _shopStatsData = new ShopStatsData();

        private async Task GetMainShopPlan()
        {
            IsLoading = true;
            var response = await ClientDbProvider.GetMainShopPlan(ShopId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<ShopStatsData>(response, "Загрузка главного плана").WaitAsync(CancellationToken.None);
            if (result != null)
            {
                ChartClear();
                ShopStatsData = result;
                SetChartLabels(ShopStatsData.Day.Select(x => x.ToString("dd.MM.yyyy")).ToList());
                var daysCount = EndDate - StartDate;
                var series = new List<ISeries>();

                if (ShopStatsData.AverageBill.All(x => x != null))
                {
                    series.Add(new ColumnSeries<decimal?>
                    {
                        ZIndex = 1,
                        Fill = new SolidColorPaint
                        {
                            
                            Color = SKColors.DeepSkyBlue,
                        },
                        YToolTipLabelFormatter = point => $"{point.Model}%",
                        Name = "Среднее значение заказа",
                        Values = ShopStatsData.AverageBill,
                    });
                }
                if (ShopStatsData.AllProfit.All(x => x != null))
                {
                    series.Add(new ColumnSeries<decimal?>
                    {
                        ZIndex = 1,
                        Fill = new SolidColorPaint
                        {
                            Color = SKColors.DarkOrange,
                        },
                        YToolTipLabelFormatter = point => $"{point.Model}%",
                        Name = "Общий доход",
                        Values = ShopStatsData.AllProfit,
                    });
                }
                if (ShopStatsData.ClearProfit.All(x => x != null))
                {
                    series.Add(new ColumnSeries<decimal?>
                    {
                        ZIndex = 1,
                        Fill = new SolidColorPaint
                        {
                            Color = SKColors.Indigo,
                        },
                        YToolTipLabelFormatter = point => $"{point.Model}%",
                        Name = "Чистая прибыль",
                        Values = ShopStatsData.ClearProfit,
                    });
                }
                if (ShopStatsData.PurchasesCount.All(x => x != null))
                {
                    series.Add(new ColumnSeries<decimal?>
                    {
                        ZIndex = 1,
                        Fill = new SolidColorPaint
                        {
                            Color = SKColors.Salmon,
                        },
                        YToolTipLabelFormatter = point => $"{point.Model}%",
                        Name = "Количество транзакций",
                        Values = ShopStatsData.PurchasesCount,
                    });
                }
                Sections = new()
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

                YAxes =  new()
                {
                    new Axis()
                    {
                         Labeler = (value) => value.ToString() + "%",
                         LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                         SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                         MinLimit = 0
                    }
                };

                SetChartCollection(series);

            }                
            GetPlanAtributesCollection();
            IsLoading = false;
            return;
        }
        [ObservableProperty]
        private List<PlanAtribute> _planAtributesCollection;
        private async void GetPlanAtributesCollection()
        {
            IsLoading = true;
            var response = await ClientDbProvider.GetPlanAtributesCollection().WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<PlanAtribute>>(response, "Загрузка коллекции атрибутов").WaitAsync(CancellationToken.None);
            if (result != null)
            {
                PlanAtributesCollection = result;
            }
                
            IsLoading = false;
            return;
        }

        [ObservableProperty]
        private List<RectangularSection> _sections;



        private PlanAtribute _selectedPlanAtribute;

        public PlanAtribute SelectedPlanAtribute
        {
            get => _selectedPlanAtribute;
            set
            {
                SetProperty(ref _selectedPlanAtribute, value);
                if (_selectedPlanAtribute != null)
                {
                    GetAtributeObjects(_selectedPlanAtribute);
                    
                }
                else
                {
                    AtributedShopPlansCollection = [];
                }
                    

            }
        }

        [ObservableProperty]
        private List<AtributeObject> _atributeObjectsCollection;

        private async void GetAtributeObjects(PlanAtribute planAtribute)
        {
            await GetAtributedShopPlans(planAtribute).WaitAsync(CancellationToken.None);
            var response = await ClientDbProvider.GetAtributeObjectsCollection(ShopId, planAtribute.PlanAtributeId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<AtributeObject>> (response, "Загрузка атрибута: " + planAtribute.AtributeViewName).WaitAsync(CancellationToken.None);
            if (result != null)
            {
                ChartClear();
                AtributeObjectsCollection = result;
                SetChartLabels(AtributeObjectsCollection.Select(x => x.Day.ToString("dd.MM.yyyy")).ToList());

                var collection = new List<ObservablePoint>();

                if (AtributedShopPlansCollection.Count != 0)
                {


                    collection.AddRange(AtributedShopPlansCollection.OrderBy(item => item.SetTime).ToList().Select(x => new ObservablePoint(AtributeObjectsCollection.FindLastIndex(z => z.Day.Day == x.SetTime.Day), (double?)x.AtributeValue)));
                    
                    var isFirstExist = collection.FirstOrDefault(x => x.X == 0);
                    if (isFirstExist == null)
                        collection.Insert(0, new ObservablePoint(-1, (double?)AtributedShopPlansCollection.MinBy(x => x.SetTime).AtributeValue));
                    collection.Add(new ObservablePoint(AtributeObjectsCollection.Count, (double?)AtributedShopPlansCollection.MaxBy(x => x.SetTime).AtributeValue));
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
                        Fill = new SolidColorPaint
                        {

                            Color = GetAtributeColor(planAtribute.AtributeName),
                        },
                        ZIndex = 0,
                        Name = planAtribute.AtributeViewName,
                        Values = AtributeObjectsCollection.Select(x => x.ArtibuteValue),

                    },
                };

                SetChartCollection(series);
                
                YAxes = new()
                {
                    new Axis()
                    {
                         
                         LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                         SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                         MinLimit = 0
                    }
                };
            }
                
            return;
        }


        [ObservableProperty]
        private List<ShopPlan> _atributedShopPlansCollection;

        private async Task GetAtributedShopPlans(PlanAtribute planAtribute)
        {
            var response = await ClientDbProvider.GetAtributedShopPlansCollection(ShopId, planAtribute.PlanAtributeId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<ShopPlan>>(response, "Загрузка планов атрибута: " + planAtribute.AtributeViewName).WaitAsync(CancellationToken.None);
            if (result != null)
                AtributedShopPlansCollection = result;
            return;
        }

        private SKColor GetAtributeColor(string atributeName)
        {
            var color = SKColors.Lavender;
            switch (atributeName)
            {
                case "AverageBill":
                    color = SKColors.DeepSkyBlue;
                    break;
                case "AllProfit":
                    color = SKColors.DarkOrange;
                    break;
                case "ClearProfit":
                    color = SKColors.Indigo;
                    break;
                case "PurchasesCount":
                    color = SKColors.Salmon;
                    break;
            }
            return color;
        }

        [ObservableProperty]
        public List<ISeries> _series = [];

        [ObservableProperty]
        private List<Axis> _YAxes;


        private void SetChartLabels(List<string> labels)
        {
            XAxes[0].Labels = labels;
            OnPropertyChanged(nameof(XAxes));
        }

        private void SetChartCollection(ISeries seriesCollection)
        {
            Series = new()
            {
                seriesCollection
            };
        }
        private void SetChartCollection(List<ISeries> seriesCollection)
        {


            Series = new(seriesCollection);

        }

        private void ChartClear()
        {
            YAxes = [];
            Sections = [];
        }

        private List<Axis> _XAxes = new()
        {
            new Axis
            {

                LabelsRotation = 45,
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                ForceStepToMin = true,
                MinStep = 1
            }
        };
        public List<Axis> XAxes
        {
            get => _XAxes;
        }
    }
}
