using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopProject.API.Models;

namespace ShopProject.API.Helpers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ControllerEFtoSwagger : Controller
    {
        [HttpGet, Route("[controller]")]
        public abstract Task<IActionResult> Index();

        [HttpGet, Route("[controller]/Details")]
        public abstract Task<IActionResult> Details(int? id);

        [HttpGet, Route("[controller]/Create")]
        public abstract IActionResult Create();

        [HttpPost, Route("[controller]/Create")]
        public abstract Task<IActionResult> Create([Bind("ProductId,CategoryId,ProductName,Code,BuyCost,SellCost,Barcode")] Product product);


        [HttpGet, Route("[controller]/Edit")]
        public abstract Task<IActionResult> Edit(int? id);


        [HttpPost, Route("[controller]/Edit")]
        public abstract Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Code,BuyCost,SellCost,Barcode")] Product product);


        [HttpGet, Route("[controller]/Delete")]
        public abstract Task<IActionResult> Delete(int? id);


        // POST: Products/Delete/5
        [HttpPost, Route("[controller]/Delete"), ActionName("Delete")]
        public abstract Task<IActionResult> DeleteConfirmed(int id);
       
    }
}
