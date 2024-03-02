using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB;
using ShopProject.EFDB.Models;

namespace ShopProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestTablesController : Controller
    {
        private readonly ServerAPIDbContext _context;

        public TestTablesController(ServerAPIDbContext context)
        {
            _context = context;
        }

        // GET: TestTables/Select
        [HttpGet("Select")]
        public async Task<IActionResult> Select()
        {
            return Json(await _context.TestTables.ToListAsync());
        }


        // POST: TestTables/Create
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestId,TestText")] TestTable testTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Json(testTable);
        }


        // POST: TestTables/Update
        [HttpPost("Update")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("TestId,TestText")] TestTable testTable)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestTableExists(testTable.TestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Json(testTable);
        }


        // POST: TestTables/Delete/5
        [HttpPost("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testTable = await _context.TestTables.FindAsync(id);
            if (testTable != null)
            {
                _context.TestTables.Remove(testTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestTableExists(int id)
        {
            return _context.TestTables.Any(e => e.TestId == id);
        }
    }
}
