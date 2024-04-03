﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Xml.Linq;
using System.Net.Http;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using NuGet.Packaging;
using ShopProject.EFDB.Models;
using System.Drawing;
using System.Collections;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ServerDbController : Controller
    {
        JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };


        private readonly ServerAPIDbContext _context;

        public ServerDbController(ServerAPIDbContext context)
        {
            _context = context;

            LoadAll();
        }

        private void LoadAll()
        {
            

        }

        [HttpGet("SingIn")]
        public IActionResult SingIn(string login, string password)
        {
            _context.Roles.Load();
            var user = _context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            
            if (user == null) { return BadRequest(); }          
            
            return Json(user, _options);
        }


        [HttpPost("InitializeDataBase")]
        public IActionResult InitializeDataBase()
        {

            DbFiller.InitDb(_context);

            return Ok();
        }


        // GET: api/<DbController>/Select/entityTypeName
        [HttpGet("Select")]
        public async Task<IActionResult> Select(string entityTypeName)
        {

            PropertyInfo? dbSetProperty = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType && 
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && 
                p.PropertyType.GetGenericArguments()[0].Name == entityTypeName);

            var entityType = _context.GetType().GetProperties().FirstOrDefault(p => p.PropertyType.GetGenericArguments()[0].Name == entityTypeName).PropertyType.GetGenericArguments()[0];

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
                return Json(results, _options);
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
                return Json(entity, _options);

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            

        }
    }
}
