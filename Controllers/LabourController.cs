using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD3.Data;
using NBD3.Models;

namespace NBD3.Controllers
{
    [Authorize(Roles = "Admin, General Manager")]
    public class LabourController : Controller
    {
        private readonly NBDContext _context;

        public LabourController(NBDContext context)
        {
            _context = context;
        }

        // GET: Labour
        public async Task<IActionResult> Index()
        {
              return View(await _context.Labours.ToListAsync());
        }

        // GET: Labour/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Labours == null)
            {
                return NotFound();
            }

            var labour = await _context.Labours
                .FirstOrDefaultAsync(m => m.LabourID == id);
            if (labour == null)
            {
                return NotFound();
            }

            return View(labour);
        }

        // GET: Labour/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LabourID,LabourType,LabourPriceHour,LabourCostHour")] Labour labour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labour);
        }

        // GET: Labour/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Labours == null)
            {
                return NotFound();
            }

            var labour = await _context.Labours.FindAsync(id);
            if (labour == null)
            {
                return NotFound();
            }
            return View(labour);
        }

        // POST: Labour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LabourID,LabourType,LabourPriceHour,LabourCostHour")] Labour labour)
        {
            if (id != labour.LabourID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabourExists(labour.LabourID))
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
            return View(labour);
        }

        // GET: Labour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Labours == null)
            {
                return NotFound();
            }

            var labour = await _context.Labours
                .FirstOrDefaultAsync(m => m.LabourID == id);
            if (labour == null)
            {
                return NotFound();
            }

            return View(labour);
        }

        // POST: Labour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Labours == null)
            {
                return Problem("Entity set 'NBDContext.Labours'  is null.");
            }
            var labour = await _context.Labours.FindAsync(id);
            if (labour != null)
            {
                _context.Labours.Remove(labour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabourExists(int id)
        {
          return _context.Labours.Any(e => e.LabourID == id);
        }
    }
}
