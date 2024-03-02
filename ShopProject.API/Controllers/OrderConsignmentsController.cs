using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopProject.EFDB.Models;

namespace ShopProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderConsignmentsController : Controller
    {
        private readonly ServerAPIDbContext _context;

        public OrderConsignmentsController(ServerAPIDbContext context)
        {
            _context = context;
        }

        // GET: OrderConsignments/Select
        [HttpGet("Select")]
        public async Task<IActionResult> Select()
        {
            var serverAPIDbContext = _context.OrderConsignments.Include(o => o.Worker);
            return Json(await serverAPIDbContext.ToListAsync());
        }

        // POST: OrderConsignments/Create
         [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderConsignmentId,WorkerId,DateTime")] OrderConsignment orderConsignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderConsignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Fullname", orderConsignment.WorkerId);
            return Json(orderConsignment);
        }


        // POST: OrderConsignments/Update
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("OrderConsignmentId,WorkerId,DateTime")] OrderConsignment orderConsignment)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderConsignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderConsignmentExists(orderConsignment.OrderConsignmentId))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Fullname", orderConsignment.WorkerId);
            return Json(orderConsignment);
        }

       

        // POST: OrderConsignments/Delete/5
        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderConsignment = await _context.OrderConsignments.FindAsync(id);
            if (orderConsignment != null)
            {
                _context.OrderConsignments.Remove(orderConsignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderConsignmentExists(int id)
        {
            return _context.OrderConsignments.Any(e => e.OrderConsignmentId == id);
        }
    }
}
