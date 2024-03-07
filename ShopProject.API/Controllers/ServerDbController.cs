using Microsoft.AspNetCore.Mvc;
using ShopProject.EFDB.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using System.Net.Http;

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
        // GET: api/<DbController>/Select/entityTypeName
        [HttpGet("Select")]
        public async Task<IActionResult> Select(string entityTypeName)
        {

            PropertyInfo? dbSetProperty = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType && 
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && 
                p.PropertyType.GetGenericArguments()[0].Name == entityTypeName);

            if (dbSetProperty != null)
            {
                var dbSet = dbSetProperty.GetValue(_context);
                MethodInfo toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethod("ToListAsync")
                    .MakeGenericMethod(dbSetProperty.PropertyType.GetGenericArguments()[0]);

                var results = await (dynamic)toListAsyncMethod.Invoke(null, new object[] { dbSet, null });
                return Json(results);
            }

            return BadRequest("Invalid entity type name");

        }

        
        // POST api/<DbController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(dynamic stringContent)
        {
            return await CUD(stringContent, "Create");
        }

        // PUT api/<DbController>/Update
        [HttpPost("Update")]
        public async Task<IActionResult> Update(string jsonEntity, string entityTypeName)
        {
            //return await CUD(jsonEntity, entityTypeName, "Update");
            return Ok();
        }

        // DELETE api/<DbController>/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string jsonEntity, string entityTypeName)
        {
            //return await CUD(jsonEntity, entityTypeName, "Delete");
            return Ok();
        }
        private Type GetEntityType(string entityTypeName)
        {
            Type? entityType = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                p.PropertyType.GetGenericArguments()[0].Name == entityTypeName)
                .PropertyType.GetGenericArguments()[0];
            return entityType;
        }
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        private static object DeserilizeEntity(string jsonEntity, Type entityType)
        {

            return JsonSerializer.Deserialize(jsonEntity, entityType, options);
        }
        private async Task<IActionResult> CUD(dynamic stringContent, string operationName)
        {

            //var JsonContent = await stringContent.ReadAsStringAsync();
            var jsonEntity = (string)stringContent.jsonEntity;
            var entityTypeName = (string)stringContent.entityTypeName;
            //var result = Newtonsoft.Json.JsonConvert.DeserializeObject(JsonContent);
            
            Type entityType = GetEntityType(entityTypeName);
            if (entityType == null)
                return BadRequest("Invalid entity type name");
            var entity = DeserilizeEntity(jsonEntity, entityType);
            if (entity == null)
                return BadRequest("Deserialize was failed");
            switch (operationName)
            {
                case "Create":
                    _context.Add(entity);
                    break;
                case "Update":
                    _context.Update(entity);
                    break;
                case "Delete":
                    _context.Remove(entity);
                    break;
            }
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
