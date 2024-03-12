using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Tests.DbContextTests
{
    [TestClass]
    public class EntityListTest
    {
        [TestMethod]
        public async Task FillEntityListTest()
        {
            EntityList<TestTable> entityList = [];
            await entityList.Fill();

            Assert.IsTrue(entityList.Any());

        }
        [TestMethod]
        public async Task AddEntityListTest()
        {
            EntityList<TestTable> entityList = [];
            await entityList.Fill();
            entityList.Add(new TestTable() { TestText = "AddEntityListTest"});
            Assert.IsTrue(entityList.Any());

        }
    }
}
