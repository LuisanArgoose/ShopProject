using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.DevelopmentSettings
{
    public partial class FillDbSettings : ObservableObject
    {

        public FillDbSettings()
        {
            FillDbCommand = new AsyncRelayCommand(FillDb);
        }
        public IAsyncRelayCommand FillDbCommand { get; }
        private async Task FillDb()
        {
            await ClientDbProvider.FillDb(DateTime.Now.AddDays(-30), DateTime.Now);
        }
    }
}
