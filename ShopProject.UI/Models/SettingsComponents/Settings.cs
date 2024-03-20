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
        private Settings() { }
        [ObservableProperty]
        public SettingsModel _settingsModel = new SettingsModel();

        private static Settings _instance;
        static Settings()
        {
            _instance = new Settings();
        }

        public static bool SaveInstance()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SettingsModel));
                using (TextWriter writer = new StreamWriter("settings.xml"))
                {
                    serializer.Serialize(writer, _instance.SettingsModel);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool LoadInstance()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SettingsModel));
                using (TextReader reader = new StreamReader("settings.xml"))
                {
                    _instance.SettingsModel = (SettingsModel)serializer.Deserialize(reader);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static Settings GetInstance()
        {
            return _instance;
        }
    }
}
