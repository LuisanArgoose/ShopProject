

using NuGet.ContentModel;
using ShopProject.API.Controllers;
using ShopProject.EFDB.Models;
using ShopProject.API.Data;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;
namespace ShopProject.Tests.DbContextTests
{
    [TestClass]
    public class ServerAPIDbContextTest
    {
        private readonly ServerAPIDbContext _serverExample;

        public ServerAPIDbContextTest()
        {
            _serverExample = new ServerAPIDbContext();
        }
        
        
    }
}