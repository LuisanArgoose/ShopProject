using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Xml.Linq;
using System.Net.Http;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using NuGet.Packaging;
using ShopProject.EFDB.Models;
using System.Drawing;
using System.Collections;
using System.Globalization;
using ShopProject.EFDB.DataModels;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServerDbController : Controller
    {
        JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };


        private readonly ServerAPIDbContext _context;

        public ServerDbController(ServerAPIDbContext context)
        {
            _context = context;

            LoadAll();
            
        }

        private void LoadAll()
        {

        }

        [HttpGet("SingIn")]
        public IActionResult SingIn(string login, string password)
        {
            _context.Users.Load();
            _context.Roles.Load();
            var user = _context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            
            if (user == null) { return BadRequest(); }          
            
            return Json(user, _options);
        }


        [HttpGet("InitializeDataBase")]
        public IActionResult InitializeDataBase()
        {

            DbFiller.InitDb(_context);

            return Ok();
        }

        [HttpGet("FillDataBase")]
        public IActionResult FillDataBase(string startDate, string endDate)
        {
            var test = DateTime.Parse(startDate);
            DbFiller.FillDb(_context,
                DateTime.Parse(startDate),
                DateTime.Parse(endDate));
            
            return Ok();
        }


        [HttpGet("GetShopAverageBill")]
        public IActionResult GetShopAverageBill(int shopId, string startDate, string endDate)
        {

            DateTime startDateD = DateTime.Parse(startDate);
            DateTime endDateD = DateTime.Parse(endDate);
            void Swap<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }
            if (startDateD > endDateD)
            {
                Swap(ref startDateD, ref endDateD);
            }

            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if(shop == null) { return BadRequest("Shop not found"); }
            List<ShopAverageBill> averageBillList = [];
            var allPurchasesInShop = _context.Purchases
                .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                    x.OperationTime.Date >= startDateD &&
                    x.OperationTime.Date <= endDateD);
            DateTime currentDate = startDateD;
            while (currentDate <= endDateD)
            {
                var selectedDay = currentDate;
                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);
                var daysAllProfit = daysPurchaseProducts.Select(x => x.Count * x.Product.SellPrice).Sum();

                var daysPurchasesCount = daysPurchaseProducts.Count();

                var daysClearProfit = daysPurchaseProducts.Select(x => x.Count * (x.Product.SellPrice - x.Product.CostPrice)).Sum();

                averageBillList.Add(new ShopAverageBill()
                {
                    AverageBill = daysPurchasesCount != 0 ? daysAllProfit / daysPurchasesCount : 0,
                    PurchasesCount = daysPurchasesCount,
                    AllProfit = daysAllProfit,
                    ClearProfit = daysClearProfit,
                    Day = selectedDay
                });
                currentDate = currentDate.AddDays(1);
            }

            return Json(averageBillList, _options);
        }

        [HttpGet("GetShopsCollection")]
        public IActionResult GetShopsCollection()
        {
            var shopCollection = _context.Shops.Select(x => x);

            return Json(shopCollection, _options);
        }


        [HttpGet("GetShopInfo")]
        public IActionResult GetShopInfo(int shopId)
        {
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if (shop == null)
                return BadRequest();
            return Json(shop, _options);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            var shop = _context.Shops.First();
            List<ShopAverageBill> averageBillList = new List<ShopAverageBill>();
            var allPurchasesInShop = _context.Purchases
                .Where(x => x.Cashier.Shop.ShopId == shop.ShopId && 
                    x.OperationTime.Date > DateTime.Today.AddDays(-31).Date);
            for(int i = 0; i <= 30; i++)
            {
                var selectedDay = DateTime.Today.AddDays(-i).Date;
                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);
                var daysAllProfit = daysPurchaseProducts.Select(x => x.Count * x.Product.SellPrice).Sum();

                var daysPurchasesCount = daysPurchaseProducts.Count();

                var daysClearProfit = daysPurchaseProducts.Select(x => x.Count * (x.Product.SellPrice - x.Product.CostPrice)).Sum();

                averageBillList.Add(new ShopAverageBill()
                {
                    AverageBill = daysPurchasesCount != 0 ? daysAllProfit / daysPurchasesCount : 0,
                    PurchasesCount = daysPurchasesCount,
                    AllProfit = daysAllProfit,
                    ClearProfit = daysClearProfit,
                    Day = selectedDay
                });
            }

            return Json(averageBillList, _options);
        }


        
    }
}
