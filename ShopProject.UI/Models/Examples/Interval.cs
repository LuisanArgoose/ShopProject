using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UI.Models.Examples
{
    public class Interval
    {
        public string Name = null!;
        public string View = null!;
        public override string ToString()
        {
            return View;
        }
    }
}
