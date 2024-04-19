using LiveChartsCore.SkiaSharpView.Painting;
using ShopProject.UI.ViewModels.Examples;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;

namespace ShopProject.UI.Views.Examples
{
    /// <summary>
    /// Логика взаимодействия для ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {

        public ShopView()
        {

            InitializeComponent();
            ShopChart.LegendTextPaint = new SolidColorPaint(new SKColor(255, 255, 255));


        }


    }
}
