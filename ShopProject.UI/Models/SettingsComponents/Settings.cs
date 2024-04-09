using ShopProject.EFDB.Models;
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
        public static User GetActiveUser()
        {
            return _instance.ActiveUser;
        }
        public static void SetActiveUser(User user)
        {
            _instance.ActiveUser = user;
        }
        public static void Exit()
        {
            _instance.ActiveUser = GetEmptyUser();
            
        }
        private static User GetEmptyUser()
        {
            return new()
            {
                Fullname = "UnLogin",
                Role = new()
                {
                    IsAdmin = false,
                    IsSalesManager = false,
                    IsShopManager = false
                },
            };
        }
        private Settings() 
        {
            ActiveUser = GetEmptyUser();
            SettingsModel = new SettingsModel();
        }


        private SettingsModel _settingsModel;

        public SettingsModel SettingsModel
        {
            get => _settingsModel;
            set
            {
                SetProperty(ref _settingsModel, value);
                _settingsModel.PropertyChanged += SettingsChanged;
            }
        }

        private void SettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(sender)));
        }

        private User _activeUser;

        public User ActiveUser
        {
            get => _activeUser;
            set
            {
                SetProperty(ref _activeUser, value);
                OnPropertyChanged(nameof(ProfilePageType));
            }
        }

        public Type ProfilePageType
        {
            get
            {
                if(ActiveUser.Fullname == "UnLogin")
                {
                    return typeof(ProfilePage);
                }
                else
                {
                    return typeof(ActiveProfilePage);
                }
            }
        }
        
    }
}
