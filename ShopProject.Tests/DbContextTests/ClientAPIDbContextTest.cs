
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Helpers;
using ShopProject.EFDB.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests.DataBaseTests
{
    [TestClass]
    public class ClientAPIDbContextTest
    {
        private ClientAPIDbContext _clientExample;

        public ClientAPIDbContextTest()
        {
            _clientExample = new ClientAPIDbContext();
        }

        [TestMethod]
        public async Task APISelectTest()
        {

            await _clientExample.TestTables.Fill();
            var result = _clientExample.TestTables.First();
            Assert.AreEqual("Test", result.TestText);
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
        [TestMethod]
        public async Task DeseriliseModelTest()
        {
            var collectionJson = "[{ \"TestId\":1,\"TestText\":\"Test\"},{ \"TestId\":2,\"TestText\":\"Test\"}]";
            var collection = JsonSerializer.Deserialize<List<TestTable>>(collectionJson);
            Assert.AreEqual("Test", collection.First().TestText);
        }
    }
}