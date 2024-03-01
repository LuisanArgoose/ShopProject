
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Models;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests
{
    [TestClass]
    public class DataBaseTest
    {
        private ServerAPIDbContext _serverExample;
        private ClientAPIDbContext _clientExample;

        public DataBaseTest() {
            
            
 
        }

        [TestMethod]
        public void ConnectionTest()
        {
            _serverExample = new ServerAPIDbContext();
            var result =  _serverExample.Categories.First();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AddToDbSetTest()
        {
            _serverExample = new ServerAPIDbContext();
            _serverExample.Categories.Load();
            _serverExample.Categories.Add(new Category
            {
                CategoryId = -1,
                CategoryName = "Test",
                Products = []
            });
            
            var result = _serverExample.Categories.Where(x => x.CategoryName == "Test").ToList() ;
            Assert.IsNotNull(result.Count > 0 ? true : null);
        }
        [TestMethod]
        public async Task APISelectTest()
        {
            _clientExample = new ClientAPIDbContext("https://localhost:7178/api/");
            await _clientExample.FillCollections();
            var result = _clientExample.Categories.First();
            Assert.AreEqual("Нижнее бельё", result.CategoryName);
        }
        [TestMethod]
        public async Task APICreateTest()
        {
            _clientExample = new ClientAPIDbContext("https://localhost:7178/api/");
            await _clientExample.FillCollections();
            var result = _clientExample.Categories.First();
            Assert.AreEqual("Нижнее бельё", result.CategoryName);
        }
    }
}