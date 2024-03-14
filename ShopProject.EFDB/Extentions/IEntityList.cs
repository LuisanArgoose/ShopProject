using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.EFDB.Extentions
{
    public interface IEntityList : IBindingList
    {
        public Task Fill();
    }
}
