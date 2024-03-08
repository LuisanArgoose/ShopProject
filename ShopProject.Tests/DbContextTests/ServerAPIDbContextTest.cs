

using NuGet.ContentModel;
using ShopProject.API.Controllers;
using ShopProject.EFDB.Models;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests.DataBaseTests
{
    [TestClass]
    public class ServerAPIDbContextTest
    {
        private readonly ServerAPIDbContext _serverExample;
        private readonly ServerDbController _testController;

        public ServerAPIDbContextTest()
        {

            _serverExample = new ServerAPIDbContext();
            _testController = new ServerDbController(_serverExample);
        }

        private TestTable GetTestExample(string testMark)
        {
            return new TestTable()
            {
                TestText = testMark
            };
        }
        private StringContent? GetTestContentCUD(string testMark)
        {
            //Создать Entity
            var testTable = new TestTable()
            {
                TestText = testMark
            };

            //Обернуть содержание и тип в content 
            string jsonEntity = JsonSerializer.Serialize(testTable);
            string entityTypeName = testTable.GetType().Name;
            var requestData = new
            {
                jsonEntity,
                entityTypeName

            };
            string requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            return new StringContent(requestDataJson, Encoding.UTF8, "application/json");
        }

        [TestMethod]
        public async Task DeserializingAPostRequestTest()
        {

            string testMark = "DeserializeTest";
            var content = GetTestContentCUD(testMark);
            //Отправить content в тестируемый метод
            var entity = await _testController.DeserilizeEntity(content);

            Assert.AreEqual(testMark, (entity as TestTable).TestText);

        }
        [TestMethod]
        public async Task ServerCreateTest()
        {
            
            string testMark = "ServerCreateTest";
            if(_serverExample.TestTables.Any(t => t.TestText == testMark))
            {
                var ifExist = _serverExample.TestTables.First(t => t.TestText == testMark);
                if (ifExist != null)
                    _serverExample.Remove(ifExist);
            }
            var content = GetTestContentCUD(testMark);
            await _testController.Create(content);
            var ifExistNow = _serverExample.TestTables.Any(t => t.TestText == testMark);
            Assert.IsTrue(ifExistNow);
        }
        [TestMethod]
        public async Task ServerUpdateTest()
        {

            string testMark = "ServerUpdateTest";
            if (!_serverExample.TestTables.Any(t => t.TestText == testMark))
            {
                var ifExist = _serverExample.TestTables.First(t => t.TestText == testMark);
                if (ifExist != null)
                    _serverExample.Remove(ifExist);
            }
            var content = GetTestContentCUD(testMark);
            await _testController.Create(content);
            var ifExistNow = _serverExample.TestTables.Any(t => t.TestText == testMark);
            Assert.IsTrue(ifExistNow);
        }

    }
}