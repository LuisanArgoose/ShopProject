
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Helpers;
using ShopProject.EFDB.Models;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests.DataBaseTests
{
    [TestClass]
    public class ClientAPIDbContextTest
    {
        private ClientAPIDbContext _clientExample;

        public ClientAPIDbContextTest()
        {
            _clientExample = new ClientAPIDbContext("https://localhost:7178/api/");
        }

        [TestMethod]
        public async Task APISelectTest()
        {
           
            await _clientExample.TestTables.FillAsync();
            //_clientExample.Categories.Load();
            var result = _clientExample.Categories.First();
            Assert.AreEqual("Нижнее бельё", result.CategoryName);
        }
        [TestMethod]
        public async Task APICreateTest()
        {
            
            //await _clientExample.FillCollections();
            var testTable = new TestTable()
            {
                TestText = "AddTestExample"
            };
            var result = _clientExample.TestTables.Add(testTable);
            //Assert.AreEqual("Нижнее бельё", result.TestText);
        }
        
    }
}