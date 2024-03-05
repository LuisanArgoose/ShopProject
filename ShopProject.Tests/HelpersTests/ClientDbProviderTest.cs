using ShopProject.EFDB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Tests.HelpersTests
{
    [TestClass]
    public class ClientDbProviderTest
    {
        private readonly ClientDbProvider _clientDbProvider = ClientDbProvider.GetInstance();
        [TestMethod]
        public async Task BadOperationNameTest()
        {
            try
            {
                await _clientDbProvider.PostCRD("awd", "randomoperation");
            }
            catch(Exception e)
            {
                Assert.AreEqual("Bad operation name", e.Message);
                return;
            }
            Assert.Fail();
        }
    }
}
