
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Helpers;
using ShopProject.EFDB.Models;
using System.Collections.Generic;
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
        private TestTable GetTestExample(string testMark)
        {
            return  new TestTable()
            {
                TestText = testMark,
                TextToUpdate = DateTime.Now.ToString()
            };
        }

        [TestMethod]
        public async Task APISelectTest()
        {

            await _clientExample.TestTables.Fill();
            var result = _clientExample.TestTables.First();
            Assert.AreEqual("Test", result.TestText);
        }
        [TestMethod]
        public async Task ClientCreateTest()
        {
            await _clientExample.TestTables.Fill();

            string testMark = "ClientCreateTest";
            /*var marks = _clientExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Any())
            {
                _clientExample.RemoveRange(marks);
                await _clientExample.SaveChangesAPIAsync();
            }
            await _clientExample.TestTables.Fill();
            marks = _clientExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Any())
            {
                Assert.Fail();
            }*/
            var entity = GetTestExample(testMark);
            _clientExample.TestTables.Add(entity);
            await _clientExample.SaveChangesAPIAsync();
            await _clientExample.TestTables.Fill();
            var ifExistNow = _clientExample.TestTables.Any(t => t.TestText == testMark && t.TextToUpdate == entity.TextToUpdate);
            Assert.IsTrue(ifExistNow);
        }
       
        [TestMethod]
        public async Task ClientUpdateTest()
        {
            await _clientExample.TestTables.Fill();

            string testMark = "ClientCreateTest";
            var marks = _clientExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Any())
            {
                _clientExample.RemoveRange(marks);
                await _clientExample.SaveChangesAPIAsync();
            }
            await _clientExample.TestTables.Fill();
            marks = _clientExample.TestTables.Where(t => t.TestText == testMark);
            if (marks.Any())
            {
                Assert.Fail();
            }
            var entity = GetTestExample(testMark);
            _clientExample.TestTables.Add(entity);
            await _clientExample.SaveChangesAPIAsync();
            await _clientExample.TestTables.Fill();
            var ifExistNow = _clientExample.TestTables.Any(t => t.TestText == testMark && t.TextToUpdate == entity.TextToUpdate);
            Assert.IsTrue(ifExistNow);
        }
        [TestMethod]
        public async Task ClientDeleteTest()
        {
        }
        public async Task ClientCreateExistTest()
        {
        }
    }
}