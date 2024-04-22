

using NuGet.ContentModel;
using ShopProject.API.Controllers;
using ShopProject.EFDB.Models;
using ShopProject.API.Data;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests.DbContextTests
{
    [TestClass]
    public class ServerAPIDbContextTest
    {
        private readonly ServerAPIDbContext _serverExample;

        public ServerAPIDbContextTest()
        {
            _serverExample = new ServerAPIDbContext();
        }
        [TestMethod]
        public void ShortInput()
        {
            _serverExample.PlanAtributes.Load();
            _serverExample.PlanAtributes.RemoveRange(_serverExample.PlanAtributes);
            var planAtributes = new List<PlanAtribute>()
            {
                new PlanAtribute()
                {
                    AtributeName = "AverageBill",
                    AtributeViewName = "Средний чек"
                },
                new PlanAtribute()
                {
                    AtributeName = "AllProfit",
                    AtributeViewName = "Общая прибыль"
                },
                new PlanAtribute()
                {
                    AtributeName = "ClearProfit",
                    AtributeViewName = "Чистая прибыль"
                },
                new PlanAtribute()
                {

                    AtributeName = "PurchasesCount",
                    AtributeViewName = "Количество продаж"
                }
            };
            _serverExample.PlanAtributes.AddRange(planAtributes);
            _serverExample.SaveChanges();
            Assert.IsTrue(true);
        }
        
    }
}