using Microsoft.AspNetCore.Mvc;
using ShopProject.EFDB.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerDbController: Controller
    {
        private readonly ServerAPIDbContext _context;

        public ServerDbController(ServerAPIDbContext context)
        {
            _context = context;
        }
        // GET: api/<DbController>/Select/TestTableType
        [HttpGet("Select")]
        public async Task<IActionResult> Select(string tableType)
        {

            PropertyInfo? dbSetProperty = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType && 
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && 
                p.PropertyType.GetGenericArguments()[0].Name == tableType);

            if (dbSetProperty != null)
            {
                var dbSet = dbSetProperty.GetValue(_context);
                MethodInfo toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethod("ToListAsync")
                    .MakeGenericMethod(dbSetProperty.PropertyType.GetGenericArguments()[0]);

                var results = await (dynamic)toListAsyncMethod.Invoke(null, new object[] { dbSet, null });
                return Json(results);
            }

            return BadRequest("Invalid DbSet name");

        }
        // POST api/<DbController>
        [HttpPost("Create")]
        public void Create(string jsonEntity)
        {
        }

        // PUT api/<DbController>/5
        [HttpPost("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DbController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
