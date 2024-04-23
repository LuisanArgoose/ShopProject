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


        [ObservableProperty]
        private DateTime _startDate = DateTime.Today.AddDays(-15);

        [ObservableProperty]
        private DateTime _endDate = DateTime.Today;

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
                ShopStatsData = result;
                SetChartLabels(ShopStatsData.Day.Select(x => x.ToString("dd.MM.yyyy")).ToList());
                SetChartCollection(new List<ISeries>(){
                    new ColumnSeries<decimal>
                    {
                        Name = "Среднее значение заказа",
                        Values = (IEnumerable<decimal>)ShopStatsData.AverageBill,
                    },
                    new ColumnSeries<decimal>
                    {
                        Name = "Общий доход",
                        Values = (IEnumerable<decimal>)ShopStatsData.AllProfit,
                    },
                    new ColumnSeries<decimal>
                    {
                        Name = "Чистая прибыль",
                        Values = (IEnumerable<decimal>)ShopStatsData.ClearProfit,
                    },
                    new ColumnSeries<decimal>
                    {
                        Name = "Количество транзакций",
                        Values = (IEnumerable<decimal>)ShopStatsData.PurchasesCount,
                    },
                });

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
            var response = await ClientDbProvider.GetPlanAtributesCollection(ShopId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<PlanAtribute>>(response, "Загрузка коллекции атрибутов").WaitAsync(CancellationToken.None);
            if (result != null)
            {
                PlanAtributesCollection = result;
            }
                
            IsLoading = false;
            return;
        }

        


        
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
                    GetAtributedShopPlans(_selectedPlanAtribute);
                }
                    

            }
        }

        [ObservableProperty]
        private List<AtributeObject> _atributeObjectsCollection;

        private async void GetAtributeObjects(PlanAtribute planAtribute)
        {
            var response = await ClientDbProvider.GetAtributeObjectsCollection(ShopId, planAtribute.PlanAtributeId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<AtributeObject>> (response, "Загрузка атрибута: " + planAtribute.AtributeViewName).WaitAsync(CancellationToken.None);
            if (result != null)
            {
                AtributeObjectsCollection = result;
                SetChartLabels(AtributeObjectsCollection.Select(x => x.Day.ToString("dd.MM.yyyy")).ToList());
                SetChartCollection(new ColumnSeries<decimal>
                {
                    Name = planAtribute.AtributeViewName,
                    Values = AtributeObjectsCollection.Select(x => x.ArtibuteValue),
                    
                });
            }
                
            return;
        }


        [ObservableProperty]
        private List<ShopPlan> _atributedShopPlansCollection;

        private async void GetAtributedShopPlans(PlanAtribute planAtribute)
        {
            var response = await ClientDbProvider.GetAtributedShopPlansCollection(ShopId, planAtribute.PlanAtributeId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            var result = await AlertDeserializer.Deserialize<List<ShopPlan>>(response, "Загрузка планов атрибута: " + planAtribute.AtributeViewName).WaitAsync(CancellationToken.None);
            if (result != null)
                AtributedShopPlansCollection = result;
            return;
        }

        

        [ObservableProperty]
        public List<ISeries> _series = [];
        public List<Axis> YAxes
        {
            get => new()
            {
                new Axis()
                {
                     LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                     SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                     MinLimit = 0
                }
            };
        }

        private void SetChartLabels(List<string> labels)
        {
            XAxes[0].Labels = labels;
            OnPropertyChanged(nameof(XAxes));
        }

        private void SetChartCollection(ISeries seriesCollection)
        {
            Series.Clear();
            Series.Add(seriesCollection);
            OnPropertyChanged(nameof(Series));
        }
        private void SetChartCollection(List<ISeries> seriesCollection)
        {
            Series.Clear();
            Series.AddRange(seriesCollection);
            OnPropertyChanged(nameof(Series));
        }

        private List<Axis> _XAxes = new()
        {
            new Axis
            {

                //Labels = ShopAverageBills.Select(x => x.Day.ToString("dd.MM.yyyy")).ToList(),
                LabelsRotation = 45,
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                LabelsPaint = new SolidColorPaint(new SKColor(255, 255, 255)),
                // By default the axis tries to optimize the number of 
                // labels to fit the available space, 
                // when you need to force the axis to show all the labels then you must: 
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
