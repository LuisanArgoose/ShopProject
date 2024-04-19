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
            _context.Shops.Load();
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


        [HttpGet("GetShopStats")]
        public IActionResult GetShopStats(int shopId, string startDate, string endDate, string interval)
        {
            //Получаю дату начала и конца периода 
            DateTime startDateD = DateTime.Parse(startDate);
            DateTime endDateD = DateTime.Parse(endDate);

            //Вложенный метод замены переменных
            void Swap<T>(ref T a, ref T b)
            {
                T temp = a;
                a = b;
                b = temp;
            }

            //Меняю сортирую от меньшего к большему
            if (startDateD > endDateD)
            {
                Swap(ref startDateD, ref endDateD);
            }


            //Нахожу магазин по ID
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if(shop == null) { return BadRequest("Shop not found"); }

            //Список данных
            List<ShopStats> shopStatsList = GetShopStatsDay(shop, startDateD, endDateD);




            return Json(shopStatsList, _options);
        }

        private List<ShopStats> GetShopStatsDay(Shop shop, DateTime startDate, DateTime endDate)
        {

            List<ShopStats> shopStatsList = [];
            var allPurchasesInShop = _context.Purchases
                .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                    x.OperationTime.Date >= startDate &&
                    x.OperationTime.Date <= endDate);
            DateTime currentDate = startDate;
            while (currentDate <= endDate)
            {
                var selectedDay = currentDate;
                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);
                var daysAllProfit = daysPurchaseProducts.Select(x => x.Count * x.Product.SellPrice).Sum();

                var daysPurchasesCount = daysPurchaseProducts.Count();

                var daysClearProfit = daysPurchaseProducts.Select(x => x.Count * (x.Product.SellPrice - x.Product.CostPrice)).Sum();

                shopStatsList.Add(new ShopStats()
                {
                    AverageBill = daysPurchasesCount != 0 ? daysAllProfit / daysPurchasesCount : 0,
                    PurchasesCount = daysPurchasesCount,
                    AllProfit = daysAllProfit,
                    ClearProfit = daysClearProfit,
                    Day = selectedDay
                });
                currentDate = currentDate.AddDays(1);
            }
            return shopStatsList;

        }








        [HttpGet("GetShopsCollection")]
        public IActionResult GetShopsCollection()
        {
            var shopCollection = _context.Shops.Select(x => x);

            return Json(shopCollection, _options);
        }


        [HttpGet("GetPlanAtributesCollection")]
        public IActionResult GetPlanAtributesCollection(int shopId)
        {
            var planAtributesCollection = _context.ShopPlans.Where(x => x.ShopId == shopId).Select(x => x.PlanAtribute).GroupBy(x => x.PlanAtributeId);

            return Json(planAtributesCollection, _options);
        }


        [HttpGet("GetShopInfo")]
        public IActionResult GetShopInfo(int shopId)
        {
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if (shop == null)
                return BadRequest();
            return Json(shop, _options);
        }

        [HttpGet("GetPlanAtributes")]
        public IActionResult GetPlanAtributes(int shopId, int planAtributeId, int count)
        {
            //_context.PlanAtributes.Load();
            var planAtributes = _context.ShopPlans.Where(x => x.ShopId == shopId && x.PlanAtributeId == planAtributeId).Take(count);

            return Json(planAtributes, _options);
        }


        [HttpGet("Test")]
        public IActionResult Test()
        {
            /*
            var res = new List<PlanAtribute>() 
            {

                new PlanAtribute()
                {
                    AtributeName = "AverageBill"
                },
                new PlanAtribute()
                {
                    AtributeName = "AllProfit"
                },
                new PlanAtribute()
                {
                    AtributeName = "ClearProfit"
                },
                new PlanAtribute()
                {
                    AtributeName = "PurchasesCount"
                }
            };

            _context.PlanAtributes.AddRange(res);

            var shop = _context.Shops.First(x => x.ShopId == 16);
            var plan = new ShopPlan()
            {
                Shop = shop,
                AtributeValue = 595,
                PlanAtribute = _context.PlanAtributes.First(x => x.AtributeName == "AverageBill"),
                SetTime = DateTime.Now.AddDays(-10)
            };
            _context.ShopPlans.Add(plan);
            _context.SaveChanges();*/
            return Ok();
        }


    }
}
