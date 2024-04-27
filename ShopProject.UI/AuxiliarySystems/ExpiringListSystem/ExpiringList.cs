using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.UI.AuxiliarySystems.ExpiringListSystem
{
    public class ExpiringList<T> : BindingList<T>
    {
        public void AddItem(T value, TimeSpan expirationTime)
        {

            if (Application.Current == null)
                return;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Count > 9)
                    RemoveAt(Count - 1);
                Add(value);
            });
            

            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep(expirationTime);
                if (Application.Current == null)
                    return;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (Application.Current.MainWindow != null)
                        Remove(value);
                });
            });
        }
    }
}
