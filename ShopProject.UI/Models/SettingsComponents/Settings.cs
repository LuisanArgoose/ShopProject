using ShopProject.UI.Models.SettingsComponents.AlertSettings;
using ShopProject.UI.Models.SettingsComponents.APISettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopProject.UI.Models.SettingsComponents
{
    public partial class Settings : ObservableObject
    {


        private static Settings _instance = new Settings();

        public static bool SaveInstance()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (TextWriter writer = new StreamWriter("settings.xml"))
                {
                    serializer.Serialize(writer, _instance);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static Settings LoadInstance()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (TextReader reader = new StreamReader("settings.xml"))
                {
                    _instance = (Settings)serializer.Deserialize(reader);
                }
            }
            catch
            {
            }
            return _instance;
        }
        public static Settings GetInstance()
        {
            return _instance;
        }



        private Settings() 
        {

        }

        [ObservableProperty]
        private APISettingsPart _aPISettingsPart = new APISettingsPart();

        [ObservableProperty]
        private AlertSettingsPart _alertSettingsPart = new AlertSettingsPart();

        
    }
}
