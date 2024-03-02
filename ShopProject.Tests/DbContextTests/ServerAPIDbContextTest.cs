
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Models;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests.DataBaseTests
{
    [TestClass]
    public class ServerAPIDbContextTest
    {
        private ServerAPIDbContext _serverExample;

        public ServerAPIDbContextTest()
        {

            _serverExample = new ServerAPIDbContext();

        }

        [TestMethod]
        public void ConnectionTest()
        {
            
            var result = _serverExample.Categories.First();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AddToDbSetTest()
        {
            
            _serverExample.Categories.Load();
            _serverExample.Categories.Add(new Category
            {
                CategoryId = -1,
                CategoryName = "Test",
                Products = []
            });

            var result = _serverExample.Categories.Where(x => x.CategoryName == "Test").ToList();
            Assert.IsNotNull(result.Count > 0 ? true : null);
        }
       
        
    }
}