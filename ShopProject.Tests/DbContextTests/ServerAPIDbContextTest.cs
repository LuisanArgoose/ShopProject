
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Helpers;
using ShopProject.EFDB.Models;
using System.Text.Json;
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
            
            //Вот тут протестировать типо отправка через клиент



        }
        [TestMethod]
        public void AddDeserializedEntityTest()
        {
            _serverExample.TestTables.Load();
            var entity = _serverExample.TestTables.First();
            var jsonEntity = JsonSerializer.Serialize(entity);
            var entityNew = JsonSerializer.Deserialize(jsonEntity, entity.GetType());
            var result = _serverExample.Update(entityNew).State.ToString();
            //Assert.IsNotNull(result.Count > 0 ? true : null);
        }


    }
}