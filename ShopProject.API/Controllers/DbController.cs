﻿using Microsoft.AspNetCore.Mvc;
using ShopProject.EFDB.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbController: Controller
    {
        private readonly ServerAPIDbContext _context;

        public DbController(ServerAPIDbContext context)
        {
            _context = context;
        }
        // GET: api/<DbController>/Select/TestTableType
        [HttpGet("Select")]
        public async Task<IActionResult> Select(Type type)
        {
            type = typeof(TestTable);
            var entities = await _context.Set(type);
            return Json(entities);
           
        }

        // GET api/<DbController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DbController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DbController>/5
        [HttpPut("{id}")]
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
