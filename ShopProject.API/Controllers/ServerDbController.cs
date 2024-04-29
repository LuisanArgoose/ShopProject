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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        private void TrueData(ref DateTime startDateD, ref DateTime endDateD)
        {

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
        }

        [HttpGet("GetMainShopPlan")]
        public IActionResult GetMainShopPlan(int shopId, string startDate, string endDate)
        {
            //Получаю дату начала и конца периода 
            DateTime startDateD = DateTime.Parse(startDate);
            DateTime endDateD = DateTime.Parse(endDate);


            // Установка соответствующ
            TrueData(ref startDateD, ref endDateD);


            //Нахожу магазин по ID
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if(shop == null) { return BadRequest("Shop not found"); }

            //Список данных
            ShopStatsData shopStatsList = GetShopStatsDay(shop, startDateD, endDateD); // GetShopStatsDay(shop, startDateD, endDateD);




            return Json(shopStatsList, _options);
        }

        private ShopStatsData GetShopStatsDay(Shop shop, DateTime startDate, DateTime endDate)
        {

            ShopStatsData shopStatsData = new ShopStatsData();
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

                var averageBill = daysPurchasesCount != 0 ? daysAllProfit / daysPurchasesCount : 0;

                decimal? GetPlanAtributeValue(string atribute)
                {
                    var result = _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date == currentDate.Date && x.PlanAtribute.AtributeName == atribute && x.ShopId == shop.ShopId);
                    result ??= _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date < currentDate.Date && x.PlanAtribute.AtributeName == atribute && x.ShopId == shop.ShopId);
                    result ??= _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date > currentDate.Date && x.PlanAtribute.AtributeName == atribute && x.ShopId == shop.ShopId);

                    return result?.AtributeValue;

                }

                var averageBillAtributeValue = GetPlanAtributeValue("AverageBill");
                var averageBillPercent = averageBillAtributeValue != null ? (decimal?)Math.Round((decimal)(averageBill * 100 / averageBillAtributeValue), 2, MidpointRounding.AwayFromZero): null;

                var daysPurchasesCountAtributeValue = GetPlanAtributeValue("PurchasesCount");
                var daysPurchasesCountPercent = daysPurchasesCountAtributeValue != null ? (decimal?)Math.Round((decimal)(daysPurchasesCount * 100 / daysPurchasesCountAtributeValue), 2, MidpointRounding.AwayFromZero) : null;

                var daysAllProfitAtributeValue = GetPlanAtributeValue("AllProfit");
                var daysAllProfitPercent = daysAllProfitAtributeValue != null ? (decimal?)Math.Round((decimal)(daysAllProfit * 100 / daysAllProfitAtributeValue), 2, MidpointRounding.AwayFromZero) : null;

                var clearProfitAtributeValue = GetPlanAtributeValue("ClearProfit");
                var daysClearProfitPercent = clearProfitAtributeValue != null ? (decimal?)Math.Round((decimal)(daysClearProfit * 100 / clearProfitAtributeValue), 2, MidpointRounding.AwayFromZero) : null;

                shopStatsData.Day.Add(selectedDay);
                shopStatsData.AverageBill.Add((decimal?)averageBillPercent);
                shopStatsData.PurchasesCount.Add((decimal?)daysPurchasesCountPercent);
                shopStatsData.AllProfit.Add((decimal?)daysAllProfitPercent);
                shopStatsData.ClearProfit.Add((decimal?)daysClearProfitPercent);

                currentDate = currentDate.AddDays(1);
            }
            return shopStatsData;
        }








            [HttpGet("GetShopsCollection")]
        public IActionResult GetShopsCollection()
        {
            var shopCollection = _context.Shops.Select(x => x);

            return Json(shopCollection, _options);
        }


        [HttpGet("GetPlanAtributesCollection")]
        public IActionResult GetPlanAtributesCollection()
        {
            var planAtributesCollection = _context.PlanAtributes;

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

        [HttpGet("GetAtributedShopPlansCollection")]
        public IActionResult GetAtributedShopPlansCollection(int shopId, int planAtributeId, string startDate, string endDate )
        {
            //Получаю дату начала и конца периода 
            DateTime startDateD = DateTime.Parse(startDate);
            DateTime endDateD = DateTime.Parse(endDate);

            // Установка соответствующих дат
            TrueData(ref startDateD, ref endDateD);

            List<ShopPlan> plansCollection = [];

            var firstPlan = _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date <= startDateD.Date && x.ShopId == shopId && x.PlanAtributeId == planAtributeId);
            if (firstPlan != null) 
                plansCollection.Add(firstPlan);
            
            var plans = _context.ShopPlans.Where(x => x.SetTime.Date > startDateD.Date && x.SetTime.Date <= endDateD.Date &&
                x.ShopId == shopId && x.PlanAtributeId == planAtributeId);
            plansCollection.AddRange(plans);


            return Json(plansCollection, _options);
        }

        [HttpGet("GetAtributeObjectsCollection")]
        public IActionResult GetAtributeObjectsCollection(int shopId, int planAtributeId, string startDate, string endDate)
        {
            //Получаю дату начала и конца периода 
            DateTime startDateD = DateTime.Parse(startDate);
            DateTime endDateD = DateTime.Parse(endDate);

            // Установка соответствующих дат
            TrueData(ref startDateD, ref endDateD);

            //Нахожу магазин по ID
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if (shop == null) { return BadRequest("Shop not found"); }

            List<AtributeObject> atributeValuesCollection = [];

            var atribute = _context.PlanAtributes.FirstOrDefault(x => x.PlanAtributeId == planAtributeId);
            if (atribute == null)
                return BadRequest();

            atributeValuesCollection = atribute.AtributeName switch
            {
                "AverageBill" => GetAverageBillAtributesCollection(shop, startDateD, endDateD),
                "AllProfit" => GetAllProfitAtributesCollection(shop, startDateD, endDateD),
                "ClearProfit" => GetClearProfitAtributesCollection(shop, startDateD, endDateD),
                "PurchasesCount" => GetPurchasesCountAtributesCollection(shop, startDateD, endDateD),
                _ => []
            };


            return Json(atributeValuesCollection, _options);
        }

        private List<AtributeObject> GetAverageBillAtributesCollection(Shop shop, DateTime startDate, DateTime endDate)
        {
            List<AtributeObject> atributeValuesCollection = [];
            // Все покупки в данный период времени
            var allPurchasesInShop = _context.Purchases
               .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                   x.OperationTime.Date >= startDate &&
                   x.OperationTime.Date <= endDate);

            // Начальная дата
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                var selectedDay = currentDate;

                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);
                var daysAllProfit = daysPurchaseProducts.Select(x => x.Count * x.Product.SellPrice).Sum();

                var daysPurchasesCount = daysPurchaseProducts.Count();

                var averageBill = daysPurchasesCount != 0 ? Math.Round(daysAllProfit / daysPurchasesCount, 2, MidpointRounding.AwayFromZero) : 0;

                atributeValuesCollection.Add(new AtributeObject()
                {
                    Day = currentDate,
                    ArtibuteValue = averageBill
                });

                currentDate = currentDate.AddDays(1);
            }

                return atributeValuesCollection;
        }

        private List<AtributeObject> GetAllProfitAtributesCollection(Shop shop, DateTime startDate, DateTime endDate)
        {
            List<AtributeObject> atributeValuesCollection = [];
            // Все покупки в данный период времени
            var allPurchasesInShop = _context.Purchases
               .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                   x.OperationTime.Date >= startDate &&
                   x.OperationTime.Date <= endDate);

            // Начальная дата
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                var selectedDay = currentDate;

                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);
                var daysAllProfit = Math.Round(daysPurchaseProducts.Select(x => x.Count * x.Product.SellPrice).Sum(), 2, MidpointRounding.AwayFromZero);

                atributeValuesCollection.Add(new AtributeObject()
                {
                    Day = currentDate,
                    ArtibuteValue = daysAllProfit
                });

                currentDate = currentDate.AddDays(1);
            }

            return atributeValuesCollection;
        }

        private List<AtributeObject> GetClearProfitAtributesCollection(Shop shop, DateTime startDate, DateTime endDate)
        {
            List<AtributeObject> atributeValuesCollection = [];
            // Все покупки в данный период времени
            var allPurchasesInShop = _context.Purchases
               .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                   x.OperationTime.Date >= startDate &&
                   x.OperationTime.Date <= endDate);

            // Начальная дата
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                var selectedDay = currentDate;

                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);

                var daysClearProfit = Math.Round(daysPurchaseProducts.Select(x => x.Count * (x.Product.SellPrice - x.Product.CostPrice)).Sum(), 2, MidpointRounding.AwayFromZero);               

                atributeValuesCollection.Add(new AtributeObject()
                {
                    Day = currentDate,
                    ArtibuteValue = daysClearProfit
                });

                currentDate = currentDate.AddDays(1);
            }

            return atributeValuesCollection;
        }

        private List<AtributeObject> GetPurchasesCountAtributesCollection(Shop shop, DateTime startDate, DateTime endDate)
        {
            List<AtributeObject> atributeValuesCollection = [];
            // Все покупки в данный период времени
            var allPurchasesInShop = _context.Purchases
               .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                   x.OperationTime.Date >= startDate &&
                   x.OperationTime.Date <= endDate);

            // Начальная дата
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                var selectedDay = currentDate;

                var daysPurchaseProducts = _context.PurchaseProducts
                    .Where(x => x.Purchase.Cashier.Shop.ShopId == shop.ShopId &&
                    x.Purchase.OperationTime.Date == selectedDay);

                var daysPurchasesCount = daysPurchaseProducts.Count();

                
                atributeValuesCollection.Add(new AtributeObject()
                {
                    Day = currentDate,
                    ArtibuteValue = daysPurchasesCount
                });

                currentDate = currentDate.AddDays(1);
            }

            return atributeValuesCollection;
        }

        [HttpGet("DeleteShopPlan")]
        public IActionResult DeleteShopPlan(int shopPlanId)
        {
            var plan = _context.ShopPlans.FirstOrDefault(x => x.ShopPlanId == shopPlanId);
            if (plan != null)
            {
                _context.ShopPlans.Remove(plan);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
                

        }

        [HttpPost("AddShopPlan")]
        public IActionResult AddShopPlan(JsonElement content)
        {
            var jsonShopPlan = content.ToString();

            var shopPlan = JsonSerializer.Deserialize<ShopPlan>(jsonShopPlan);
            if (shopPlan == null)
                return BadRequest();
            var planToUpdate = _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date == shopPlan.SetTime.Date && 
                x.ShopId == shopPlan.ShopId && 
                x.PlanAtributeId == shopPlan.PlanAtributeId);
            if (planToUpdate != null)
                planToUpdate.AtributeValue = shopPlan.AtributeValue;
            else
                _context.ShopPlans.Add(shopPlan);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("Test")]
        public IActionResult Test(Shop shop, DateTime startDate, DateTime endDate)
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
            };*/
            return Ok();
        }
    }
}
