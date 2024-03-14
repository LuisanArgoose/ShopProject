using Microsoft.AspNetCore.Mvc;
using ShopProject.EFDB.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
//using System.Text.Json;
using System.Xml.Linq;
using System.Net.Http;
using NuGet.Protocol;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerDbController(ServerAPIDbContext context) : Controller
    {
        private readonly ServerAPIDbContext _context = context;

        // GET: api/<DbController>/SelectEntitiesName
        [HttpGet("SelectEntitiesName")]
        public async Task<IActionResult> SelectEntitiesName()
        {
            List<string> EntitiesName = [];
            await Task.Run(() =>
            {
                EntitiesName = _context.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).Select(v => v.PropertyType.GetGenericArguments()[0].Name).ToList();
            });
            
            return Json(EntitiesName);
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
                if (dbSet == null)
                {
                    return BadRequest("Not found property in dbContext");
                }
                MethodInfo? toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethod("ToListAsync");
                if(toListAsyncMethod == null)
                {
                    return BadRequest("Not found ToListAsync method");
                }
                toListAsyncMethod = toListAsyncMethod.MakeGenericMethod(dbSetProperty.PropertyType.GetGenericArguments()[0]);

                var results = await (dynamic?)toListAsyncMethod.Invoke(null, [dbSet, null]);
                return Json(results);
            }

            return BadRequest("Invalid entity type name");

        }

        
        // POST api/<DbController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] dynamic stringContent)
        {
           
            return await CUD(stringContent, "Create");
        }

        // PUT api/<DbController>/Update
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] dynamic stringContent)
        {
            return await CUD(stringContent, "Update");

        }

        // DELETE api/<DbController>/Delete
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] dynamic stringContent)
        {
            return await CUD(stringContent, "Delete");
        }
        private Type GetEntityType(string entityTypeName)
        {
            PropertyInfo? dbSet = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                p.PropertyType.GetGenericArguments()[0].Name == entityTypeName) ?? throw new Exception("Not found dbSet");
            Type? entityType = dbSet.PropertyType.GetGenericArguments()[0];
            return entityType;
        }
        
        private object DeserilizeEntity(JsonElement content)
        {
            try
            {
                dynamic stringContentJson = content.ToString();
                
                
                dynamic stringContent = Newtonsoft.Json.JsonConvert.DeserializeObject(stringContentJson);
                var jsonEntity = (string)stringContent["jsonEntity"];
                var entityTypeName = (string)stringContent["entityTypeName"];

                Type entityType = GetEntityType(entityTypeName);
                if (entityType == null)
                    return BadRequest("Invalid entity type name");
                var entity = JsonSerializer.Deserialize(jsonEntity, entityType);
                if (entity == null)
                    return BadRequest("Deserialize was failed");
                return entity;
            }
            catch(Exception e)
            {
                return e;
            }
            
        }
        private async Task<IActionResult> CUD(dynamic stringContent, string operationName)
        {
            var entity = DeserilizeEntity(stringContent);
            try
            {
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
                return Json(entity);

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            

        }
    }
}
