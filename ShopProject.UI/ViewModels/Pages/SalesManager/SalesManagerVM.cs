using ShopProject.UI.Models;
using ShopProject.UI.ViewModels.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.ViewModels.Pages.SalesManager
{
    public partial class SalesManagerVM : ObservableObject
    {
        public SalesManagerVM()
        {
            PieChartModelList.Add(new PieChartModel());
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
                //LoadMainPlan();
                //LoadMetricsList();

            }
            catch
            {

            }
        }
    }
}
