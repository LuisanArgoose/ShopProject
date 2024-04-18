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


namespace ShopProject.UI.ViewModels.Examples
{
    public partial class ShopVM : ObservableObject
    {
        [ObservableProperty]
        private List<ShopAverageBill> _shopAverageBills = new List<ShopAverageBill>();

        [ObservableProperty]
        private Shop _selectedShop;

        private int _shopId;
        public int ShopId
        {
            get => _shopId;
            set
            {
                SetProperty(ref _shopId, value);
                GetShopInfoCommand.Execute(this);
                GetShopAverageBillCommand.Execute(this);
            }
        }
        public ShopVM(int shopId)
        {
            GetShopAverageBillCommand = new AsyncRelayCommand(GetShopAverageBill);
            GetShopInfoCommand = new AsyncRelayCommand(GetShopInfo);
            StartDate = DateTime.Today.AddDays(-15);
            EndDate = DateTime.Today;
            _isAll = true;
            ShopId = shopId;            
        }

        [ObservableProperty]
        private object _interval;

        [ObservableProperty]
        private bool _isLoading;

        private bool _isAll;
        public bool IsAll
        {
            get => _isAll;
            set
            {
                SetProperty(ref _isAll, value);
                if(!value)
                    Update();
            }
        }

        private bool _isAverageBill;
        public bool IsAverageBill
        {
            get => _isAverageBill;
            set
            {
                SetProperty(ref _isAverageBill, value);
                if(!value)
                    Update();
            }
        }

        private bool _isAllProfit;
        public bool IsAllProfit
        {
            get => _isAllProfit;
            set
            {
                SetProperty(ref _isAllProfit, value);
                if(!value)
                    Update();
            }
        }

        private bool _isClearProfit;
        public bool IsClearProfit
        {
            get => _isClearProfit;
            set
            {
                SetProperty(ref _isClearProfit, value);
                if(!value)
                    Update();
            }
        }

        private bool _isPurchasesCount;
        public bool IsPurchasesCount
        {
            get => _isPurchasesCount;
            set
            {
                SetProperty(ref _isPurchasesCount, value);
                if(!value)
                    Update();
            }
        }

        private void Update()
        {
            Series.Clear();
            if (_isAverageBill || _isAll)
            {
                Series.Add(new ColumnSeries<decimal>()
                {
                    Name = "Средний чек",
                    Values = ShopAverageBills.Select(x => x.AverageBill)
                });
            }
            if (_isAllProfit || _isAll)
            {
                Series.Add(new ColumnSeries<decimal>()
                {
                    
                    Name = "Общая прибыль",
                    Values = ShopAverageBills.Select(x => x.AllProfit)
                });
            }
            if (_isClearProfit || _isAll)
            {
                Series.Add(new ColumnSeries<decimal>()
                {
                    Name = "Чистая прибыль",
                    Values = ShopAverageBills.Select(x => x.ClearProfit)
                });
            }
            if (_isPurchasesCount || _isAll)
            {
                Series.Add(new ColumnSeries<int>()
                {
                    Name = "Количество покупок",
                    Values = ShopAverageBills.Select(x => x.PurchasesCount)
                });
            }

            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
        }

        public IAsyncRelayCommand GetShopInfoCommand { get; }

        private async Task GetShopInfo()
        {
            IsLoading = true;
            var response = await ClientDbProvider.GetShopInfo(ShopId).WaitAsync(CancellationToken.None);
            if (response.IsSuccessStatusCode == false)
            {
                AlertPoster.PostErrorAlert("Данные магазина", "Не удалось получить данные");
                IsLoading = false;
                return;
            }
            var jsonShopInfo = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Shop>(jsonShopInfo);
            if (result == null)
            {
                AlertPoster.PostSystemErrorAlert("Данные магазина", "Не удалось сериализовать данные");
                IsLoading = false;
                return;
            }
            SelectedShop = result;
            IsLoading = false;
            return;
        }


        public IAsyncRelayCommand GetShopAverageBillCommand { get; }

        [ObservableProperty]
        private DateTime _startDate;

        [ObservableProperty]
        private DateTime _endDate;

        private async Task GetShopAverageBill()
        {
            IsLoading = true;
            var response = await ClientDbProvider.GetShopAverageBill(ShopId, EndDate, StartDate).WaitAsync(CancellationToken.None);
            if (response.IsSuccessStatusCode == false)
            {
                AlertPoster.PostErrorAlert("Загрузка плана", "Не удалось получить данные");
                IsLoading = false;
                return;
            }
            var jsonShopAverageBill = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<ShopAverageBill>>(jsonShopAverageBill);
            if(result == null)
            {
                AlertPoster.PostSystemErrorAlert("Загрузка плана", "Не удалось сериализовать данные");
                IsLoading = false;
                return;
            }
            ShopAverageBills = result;
            OnPropertyChanged(nameof(XAxes));
            Update();
            IsLoading = false;
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
                     
                }
            };
        }


        public List<Axis> XAxes
        {
            get => new()
            {
                new Axis
                {
                    Labels = ShopAverageBills.Select(x => x.Day.ToString("dd.MM.yyyy")).ToList(),
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
        }
    }
}
