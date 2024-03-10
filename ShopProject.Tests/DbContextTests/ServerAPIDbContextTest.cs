

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
        private readonly ServerAPIDbContext _serverExampleToController;
        private readonly ServerAPIDbContext _serverExample;
        private readonly ServerDbController _testController;

        public ServerAPIDbContextTest()
        {
            _serverExampleToController = new ServerAPIDbContext();
            _serverExample = new ServerAPIDbContext();
            _testController = new ServerDbController(_serverExampleToController);
        }

        private TestTable GetTestExample(string testMark)
        {
            return  new TestTable()
            {
                TestText = testMark,
                TextToUpdate = DateTime.Now.ToString()
            };
        }
        private StringContent? GetTestContentCUD(string testMark)
        {
            //Создать Entity
            var testTable = GetTestExample(testMark);

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
        private StringContent? GetTestContentCUD(TestTable testTable)
        {
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
        public async Task ServerCreateTest()
        {
            
            string testMark = "ServerCreateTest";
            var marks = _serverExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Any())
            {
                _serverExample.RemoveRange(marks);
                _serverExample.SaveChanges();
            }
            marks = _serverExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Any())
            {
                Assert.Fail();
            }
            var entity = GetTestExample(testMark);
            
            var content = GetTestContentCUD(entity);
            await ClientDbProvider.PostCUD(entity, "Create");
            //await _testController.Create(content);
            var ifExistNow = _serverExample.TestTables.Any(t => t.TestText == testMark && t.TextToUpdate == entity.TextToUpdate);
            Assert.IsTrue(ifExistNow);
        }
        [TestMethod]
        public async Task ServerUpdateTest()
        {

            string testMark = "ServerUpdateTest";
            var marks = _serverExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Count() != 1)
            {
                _serverExample.RemoveRange(marks);
                var testTable = GetTestExample(testMark);
                testTable.TextToUpdate = new Random().Next(0, 1000).ToString();
                _serverExample.Add(testTable);
                _serverExample.SaveChanges();
            }
            var entityId = _serverExample.TestTables.First(t => t.TestText == testMark).TestId;
            var entityToUpdate = GetTestExample(testMark);
            entityToUpdate.TestId = entityId;
            var content = GetTestContentCUD(entityToUpdate);
            await _testController.Update(content);
            _serverExample.TestTables.Load();
            var ifExistNow = _serverExample.TestTables.Any(t => t.TestText == entityToUpdate.TestText && t.TextToUpdate == entityToUpdate.TextToUpdate);
            Assert.IsTrue(ifExistNow);
        }

        [TestMethod]
        public async Task ServerDeleteTest()
        {

            string testMark = "ServerDeleteTest";
            var marks = _serverExample.TestTables.Where(t => t.TestText == testMark);
            var testTable = GetTestExample(testMark);
            if (!marks.Any())
            {
                _serverExample.Add(testTable);
                _serverExample.SaveChanges();
            }
            marks = _serverExample.TestTables.Where(t => t.TestText == testMark);
            if (!marks.Any())
            {
                Assert.Fail();
            }

            var content = GetTestContentCUD(testTable);
            await _testController.Delete(content);
            _serverExample.TestTables.Load();
            var ifExistNow = _serverExample.TestTables.Any(t => t.TestText == testMark);
            Assert.IsTrue(!ifExistNow);
        }

    }
}