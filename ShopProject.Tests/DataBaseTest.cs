
using ShopProject.EFDB;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests
{
    [TestClass]
    public class DataBaseTest
    {
        private ProjectShopDbContext _serverExample;
        private ExternalApiDbContext _clientExample;

        public DataBaseTest() {
            
            
 
        }

        [TestMethod]
        public void ConnectionTest()
        {
            _serverExample = new ProjectShopDbContext();
            var result =  _serverExample.Categories.First();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void APIConnectionTest()
        {
            HttpClient client = new HttpClient();
            _clientExample = new ExternalApiDbContext(client, "https://localhost:7178/api/");
            var result = _clientExample.Categories.ToList();
            Assert.IsNotNull(result);
        }
    }
}