using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Tests.ExtentionsTests
{
    [TestClass]
    public class EntityListTest
    {
        /*
        [TestMethod]
        public async Task FillEntityListTest()
        {
            EntityList<TestTable> entityList = [];
            await entityList.Fill();

            Assert.IsTrue(entityList.Any());

        }
        [TestMethod]
        public async Task AddDeleteEntityListTest()
        {
            string testMark = "AddDeleteEntityListTest";
            string timeMark = DateTime.Now.ToString();
            EntityList<TestTable> entityList = [];
            await entityList.Fill();
            var entities = entityList.Where(x => x.TestText == testMark);
            if (entities.Any())
            {
                foreach (var item in entities)
                {
                    entityList.Remove(item);
                }

            }
            entityList.Add(new TestTable() 
            { 
                TestText = testMark,
                TextToUpdate = timeMark
            });
            await entityList.Fill();
            entities = entityList.Where(x => x.TestText == testMark);
            if (!entities.Any())
            {
                Assert.Fail();
            }
            
            var entity = entities.First();
            entityList.ListChanged += (sender, e) =>
            {
                wait();
            };
            void wait()
            {
                Assert.IsTrue(entity.TextToUpdate == timeMark);
            }

        }
        [TestMethod]
        public async Task UpdateEntityListTest()
        {
            string testMark = "UpdateEntityListTest";
            string timeMark = DateTime.Now.ToString();
            EntityList<TestTable> entityList = [];
            await entityList.Fill();
            var entities = entityList.Where(x => x.TestText == testMark);
            if (!entities.Any())
            {
                entityList.Add(new TestTable()
                {
                    TestText = testMark,
                    TextToUpdate = DateTime.MinValue.ToString()
                });

            }
            await entityList.Fill();
            entities = entityList.Where(x => x.TestText == testMark);
            if (!entities.Any())
            {
                Assert.Fail();
            }
            var entity = entities.First();
            entity.TextToUpdate = timeMark;
            await entityList.Fill();
            entities = entityList.Where(x => x.TestText == testMark);
            if (!entities.Any())
            {
                Assert.Fail();
            }
            entityList.ListChanged += (sender, e) =>
            {
                wait();
            };
            void wait()
            {
                Assert.IsTrue(entity.TextToUpdate == timeMark);
            }

        }*/
    }
}
