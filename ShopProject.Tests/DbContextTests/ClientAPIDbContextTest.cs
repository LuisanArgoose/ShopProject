
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
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
            _clientExample = new ClientAPIDbContext("https://localhost:7178/api/");
            //await _clientExample.FillCollection();
            _clientExample.Categories.Load();
            var result = _clientExample.Categories.First();
            Assert.AreEqual("Нижнее бельё", result.CategoryName);
        }
        [TestMethod]
        public async Task APICreateTest()
        {
            _clientExample = new ClientAPIDbContext("https://localhost:7178/api/");
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