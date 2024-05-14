using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
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
using System.Windows.Controls.Primitives;
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
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Предотвращаем выключение кнопки
            
        }

        private void ToggleButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                e.Handled = true; // Предотвращаем обработку события, если кнопка уже включена
            }
        }

        private void ShopChart_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var chart = sender as CartesianChart;
            if (chart != null)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };
                //eventArg.Handled = true;
                chart.RaiseEvent(eventArg);
            }
        }
    }
}
