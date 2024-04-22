

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
                    AtributeViewName = "������� ���"
                },
                new PlanAtribute()
                {
                    AtributeName = "AllProfit",
                    AtributeViewName = "����� �������"
                },
                new PlanAtribute()
                {
                    AtributeName = "ClearProfit",
                    AtributeViewName = "������ �������"
                },
                new PlanAtribute()
                {

                    AtributeName = "PurchasesCount",
                    AtributeViewName = "���������� ������"
                }
            };
            _serverExample.PlanAtributes.AddRange(planAtributes);
            _serverExample.SaveChanges();
            Assert.IsTrue(true);
        }
        
    }
}