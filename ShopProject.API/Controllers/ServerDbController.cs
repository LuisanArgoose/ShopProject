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
using Microsoft.VisualBasic;
using ShopProject.EFDB.Migrations;
using System.Drawing.Drawing2D;


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

        [HttpGet("ClearDataBase")]
        public IActionResult ClearDataBase()
        {

            DbFiller.ClearDb(_context);

            return Ok();
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

        [HttpGet("GetMainShopInfo")]
        public IActionResult GetMainShopInfo(int shopId, int daysInterval)
        {
            //Получаю дату начала и конца периода 
            



            //Нахожу магазин по ID
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if(shop == null) { return BadRequest("Shop not found"); }

            //Список данных
            ShopStatsData shopStatsList = GetShopStatsDay(shop, daysInterval); // GetShopStatsDay(shop, startDateD, endDateD);




            return Json(shopStatsList, _options);
        }
        
        private ShopStatsData GetShopStatsDay(Shop shop, int daysInterval)
        {
            DateTime startDate = DateTime.Today.AddDays(-daysInterval);
            DateTime endDate = DateTime.Today;
            ShopStatsData shopStatsData = new ShopStatsData()
            {
                MetricsData =
                [
                    new("Продаж в день"),
                    new("Средний чек"),
                    new("Выручка в день"),
                    new("Прибыль в день"),

                    new("Количество продаж", true),
                    new("Выручка", true),
                    new("Прибыль", true),

                ]
            };
            var allPurchasesInShop = _context.Purchases
                .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                    x.OperationTime.Date >= startDate &&
                    x.OperationTime.Date <= endDate).Include("PurchaseProducts");


            
            for (int daysCount = 0; daysCount < daysInterval; daysCount++)
            {
                var selectedDay = DateTime.Today.AddDays(-daysInterval + daysCount);
                var allPurchasesInShopToday = allPurchasesInShop.Where(x => x.OperationTime.Date == selectedDay.Date);
                var daysSalesCount = allPurchasesInShopToday.Count();
                var daysRevenue = allPurchasesInShopToday.Select(x => x.PurchaseProducts.Select(v => v.Count * v.Product.SellPrice).Sum()).Sum();
                var daysProfit = allPurchasesInShopToday.Select(x => x.PurchaseProducts.Select(v => v.Count * (v.Product.SellPrice - v.Product.CostPrice) * 0.87m).Sum()).Sum();
                var averageBill = daysSalesCount != 0 ? daysRevenue / daysSalesCount : 0;


                decimal? GetMetricValue(string metric)
                {
                    var result = _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date == selectedDay.Date && x.Metric.MetricName == metric && x.ShopId == shop.ShopId);
                    result ??= _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date < selectedDay.Date && x.Metric.MetricName == metric && x.ShopId == shop.ShopId);
                    result ??= _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date > selectedDay.Date && x.Metric.MetricName == metric && x.ShopId == shop.ShopId);

                    return result?.MetricValue;

                }

                
                var daysSalesCountMetricValue = GetMetricValue("SalesCountInDay");
                var daysSalesCountPercent = daysSalesCountMetricValue != null ? (decimal?)Math.Round((decimal)(daysSalesCount * 100 / daysSalesCountMetricValue), 2, MidpointRounding.AwayFromZero) : null;
                shopStatsData.MetricsData[4].MetricValue += daysSalesCount;
                shopStatsData.MetricsData[0].MetricPlanResult += daysSalesCountPercent;

                var averageBillMetricValue = GetMetricValue("AverageBill");
                var averageBillPercent = averageBillMetricValue != null ? (decimal?)Math.Round((decimal)(averageBill * 100 / averageBillMetricValue), 2, MidpointRounding.AwayFromZero): null;
                shopStatsData.MetricsData[1].MetricValue += averageBill;
                shopStatsData.MetricsData[1].MetricPlanResult += averageBillPercent;

                var daysRevenueMetricValue = GetMetricValue("RevenueInDay");
                var daysRevenuePercent = daysRevenueMetricValue != null ? (decimal?)Math.Round((decimal)(daysRevenue * 100 / daysRevenueMetricValue), 2, MidpointRounding.AwayFromZero) : null;
                shopStatsData.MetricsData[5].MetricValue += daysRevenue;
                shopStatsData.MetricsData[2].MetricPlanResult += daysRevenuePercent;

                var profitMetricValue = GetMetricValue("ProfitInDay");
                var daysProfitPercent = profitMetricValue != null ? (decimal?)Math.Round((decimal)(daysProfit * 100 / profitMetricValue), 2, MidpointRounding.AwayFromZero) : null;
                shopStatsData.MetricsData[6].MetricValue += daysProfit;
                shopStatsData.MetricsData[3].MetricPlanResult += daysProfitPercent;

                shopStatsData.Day.Add(selectedDay);
                shopStatsData.SalesCountInDay.Add((decimal?)daysSalesCountPercent);
                shopStatsData.AverageBill.Add((decimal?)averageBillPercent);
                shopStatsData.RevenueInDay.Add((decimal?)daysRevenuePercent);
                shopStatsData.ProfitInDay.Add((decimal?)daysProfitPercent);

;
            }
           
            // Количество продаж в день
            shopStatsData.MetricsData[0].MetricValue = (decimal?)Math.Round((decimal)(shopStatsData.MetricsData[4].MetricValue / daysInterval), 2, MidpointRounding.AwayFromZero);

            //Средний чек
            shopStatsData.MetricsData[1].MetricValue = (decimal?)Math.Round((decimal)(shopStatsData.MetricsData[1].MetricValue / daysInterval), 2, MidpointRounding.AwayFromZero);


            shopStatsData.MetricsData[2].MetricValue = (decimal?)Math.Round((decimal)(shopStatsData.MetricsData[5].MetricValue / daysInterval), 2, MidpointRounding.AwayFromZero);
            shopStatsData.MetricsData[3].MetricValue = (decimal?)Math.Round((decimal)(shopStatsData.MetricsData[6].MetricValue / daysInterval), 2, MidpointRounding.AwayFromZero);
            foreach (var metric in shopStatsData.MetricsData)
            {
                if(metric.IsNonPlanedMetric == false)
                    metric.MetricPlanResult = (decimal?)Math.Round((decimal)(metric.MetricPlanResult / daysInterval - 100), 2, MidpointRounding.AwayFromZero);
            }

            return shopStatsData;
        }

        [HttpGet("GetMainShopMetrics")]
        public IActionResult GetMainShopMetrics(int shopId, int daysInterval)
        {

            //Нахожу магазин по ID
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if (shop == null) { return BadRequest("Shop not found"); }

            List<MetricData> metricsDataList = [];



            
            return Json(metricsDataList, _options);
        }
        



        [HttpGet("GetShopsCollection")]
        public IActionResult GetShopsCollection()
        {
            var shopCollection = _context.Shops.Select(x => x);

            return Json(shopCollection, _options);
        }

        
        [HttpGet("GetMetricsCollection")]
        public IActionResult GetPlanAtributesCollection()
        {
            var metricsCollection = _context.Metrics;

            return Json(metricsCollection, _options);
        }
       

        [HttpGet("GetShopInfo")]
        public IActionResult GetShopInfo(int shopId)
        {
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if (shop == null)
                return BadRequest();
            return Json(shop, _options);
        }
        
        [HttpGet("GetMetricShopPlansCollection")]
        public IActionResult GetMetricShopPlansCollection(int shopId, int metricId, int daysInterval)
        {
            DateTime startDate = DateTime.Today.AddDays(-daysInterval);
            DateTime endDate = DateTime.Today.AddDays(30);

            List<ShopPlan> plansCollection = [];

            var firstPlan = _context.ShopPlans.FirstOrDefault(x => x.SetTime.Date <= startDate.Date && x.ShopId == shopId && x.MetricId == metricId);
            if (firstPlan != null) 
                plansCollection.Add(firstPlan);
            
            var plans = _context.ShopPlans.Where(x => x.SetTime.Date > startDate.Date && x.SetTime.Date <= endDate.Date &&
                x.ShopId == shopId && x.MetricId == metricId);
            plansCollection.AddRange(plans);


            return Json(plansCollection, _options);
        }
        
        
        [HttpGet("GetMetricPlanDataCollection")]
        public IActionResult GetMetricPlanDataCollection(int shopId, int metricId, int daysInterval)
        {
            //Получаю дату начала и конца периода 
            DateTime startDate = DateTime.Today.AddDays(-daysInterval);
            DateTime endDate = DateTime.Today.AddDays(30);


            //Нахожу магазин по ID
            var shop = _context.Shops.FirstOrDefault(x => x.ShopId == shopId);
            if (shop == null) { return BadRequest("Shop not found"); }

            List<MetricPlanData> atributeValuesCollection = [];

            var metric = _context.Metrics.FirstOrDefault(x => x.MetricId == metricId);
            if (metric == null)
                return BadRequest();

            atributeValuesCollection = GetMetricValues(shop, startDate, endDate, metric.MetricName, daysInterval);


            return Json(atributeValuesCollection, _options);
        }

        private List<MetricPlanData> GetMetricValues(Shop shop, DateTime startDate, DateTime endDate, string metricName, int daysInterval)
        {
            List<MetricPlanData> MetricValuesCollection = [];
            // Все покупки в данный период времени
            var allPurchasesInShop = _context.Purchases
                .Where(x => x.Cashier.Shop.ShopId == shop.ShopId &&
                    x.OperationTime.Date >= startDate &&
                    x.OperationTime.Date <= endDate).Include("PurchaseProducts");

            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                var allPurchasesInShopToday = allPurchasesInShop.Where(x => x.OperationTime.Date == currentDate.Date);

                decimal metricValue = metricName switch
                {
                   
                    "AverageBill" => allPurchasesInShopToday.Count() != 0 ? allPurchasesInShopToday.Select(x => x.PurchaseProducts.Select(v => v.Count * v.Product.SellPrice).Sum()).Sum() / allPurchasesInShopToday.Count() : 0,
                    "SalesCountInDay" => allPurchasesInShopToday.Count() ,
                    "RevenueInDay" => allPurchasesInShopToday.Select(x => x.PurchaseProducts.Select(v => v.Count * v.Product.SellPrice).Sum()).Sum(),
                    "ProfitInDay" => allPurchasesInShopToday.Select(x => x.PurchaseProducts.Select(v => v.Count * (v.Product.SellPrice - v.Product.CostPrice) * 0.87m).Sum()).Sum(),
                    _ => 0
                };

                MetricValuesCollection.Add(new MetricPlanData()
                {
                    Day = currentDate,
                    MetricValue = metricValue
                });
                currentDate = currentDate.AddDays(1);
            }
            
            return MetricValuesCollection;
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
                x.MetricId == shopPlan.MetricId);
            if (planToUpdate != null)
                planToUpdate.MetricValue = shopPlan.MetricValue;
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
