using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.AuxiliarySystems.ExpiringListSystem
{
    class ExpiringList<T> : BindingList<T>
    {
        public void AddItem(T value, TimeSpan expirationTime)
        {
            
            Add(value);

            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep(expirationTime);
                Remove(value);
            });
        }
    }
}
