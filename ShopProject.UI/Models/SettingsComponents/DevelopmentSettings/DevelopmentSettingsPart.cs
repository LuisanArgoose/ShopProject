using ShopProject.UI.Models.SettingsComponents.APISettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.SettingsComponents.DevelopmentSettings
{
    public partial class DevelopmentSettingsPart : ObservableObject
    {
        public DevelopmentSettingsPart()
        {
            InitDbCommand = new AsyncRelayCommand(InitDb);
        }

        [ObservableProperty]
        private AutoLoginSettings _autoLoginSettings = new AutoLoginSettings();

        public IAsyncRelayCommand InitDbCommand { get; }
        private async Task InitDb()
        {
            await ClientDbProvider.InitDb();
        }

        [ObservableProperty]
        private FillDbSettings _fillDbSettings = new FillDbSettings();
    }
}
