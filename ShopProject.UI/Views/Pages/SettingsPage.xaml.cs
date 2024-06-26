﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopProject.UI.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsVM ViewModel { get; }
        public SettingsPage(
            
            SettingsVM viewModel)
        {

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            
        }

        private void ApiCardControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            APIConnectWrap.MaxWidth = e.NewSize.Width - 200;
        }
    }
}
