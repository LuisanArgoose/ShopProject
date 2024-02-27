
using ShopProject.EFDB;
namespace ShopProject.Tests
{
    [TestClass]
    public class DataBaseTest
    {
        private ProjectShopDbContext _example;

        public DataBaseTest() {
            
            _example = new ProjectShopDbContext();
 
        }

        [TestMethod]
        public void ConnectionTest()
        {
            var result =  _example.Categories.First();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void APIConnectionTest()
        {
            _example.Categories = 
            var result = _example.SaveChangesAsync();
        }
    }
}