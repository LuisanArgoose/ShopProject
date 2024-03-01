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
    public class OrderConsignmentsController : Controller
    {
        private readonly ShopProjectDbContext _context;

        public OrderConsignmentsController(ShopProjectDbContext context)
        {
            _context = context;
        }

        // GET: OrderConsignments
        public async Task<IActionResult> Index()
        {
            var shopProjectDbContext = _context.OrderConsignments.Include(o => o.Worker);
            return View(await shopProjectDbContext.ToListAsync());
        }

        // GET: OrderConsignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderConsignment = await _context.OrderConsignments
                .Include(o => o.Worker)
                .FirstOrDefaultAsync(m => m.OrderConsignmentId == id);
            if (orderConsignment == null)
            {
                return NotFound();
            }

            return View(orderConsignment);
        }

        // GET: OrderConsignments/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Fullname");
            return View();
        }

        // POST: OrderConsignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
            return View(orderConsignment);
        }

        // GET: OrderConsignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderConsignment = await _context.OrderConsignments.FindAsync(id);
            if (orderConsignment == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Fullname", orderConsignment.WorkerId);
            return View(orderConsignment);
        }

        // POST: OrderConsignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderConsignmentId,WorkerId,DateTime")] OrderConsignment orderConsignment)
        {
            if (id != orderConsignment.OrderConsignmentId)
            {
                return NotFound();
            }

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
            return View(orderConsignment);
        }

        // GET: OrderConsignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderConsignment = await _context.OrderConsignments
                .Include(o => o.Worker)
                .FirstOrDefaultAsync(m => m.OrderConsignmentId == id);
            if (orderConsignment == null)
            {
                return NotFound();
            }

            return View(orderConsignment);
        }

        // POST: OrderConsignments/Delete/5
        [HttpPost, ActionName("Delete")]
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
