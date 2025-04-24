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
using NuGet.Packaging.Signing;

namespace NBD3.Controllers
{
    [Authorize(Roles = "Admin, General Manager")]
    public class InventoryController : Controller
    {
        private readonly NBDContext _context;

        public InventoryController(NBDContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string inventoryCodeSearch, string sortField, string sortDirection, int? page)
        {
            // Retrieve all inventory items with eager loading of MaterialCategory
            var inventoryItems = _context.Inventorys.Include(i => i.MaterialCategory).AsQueryable();

            // Filter by inventory code if search string is provided
            if (!string.IsNullOrEmpty(inventoryCodeSearch))
            {
                inventoryItems = inventoryItems.Where(i => i.InventoryCode.Contains(inventoryCodeSearch));
            }

            // Default sorting by InventoryCode if no sort field is specified
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "InventoryCode";
            }

            // Sorting
            switch (sortField)
            {
                case "InventoryCode":
                    inventoryItems = sortDirection == "desc" ? inventoryItems.OrderByDescending(i => i.InventoryCode) : inventoryItems.OrderBy(i => i.InventoryCode);
                    break;
                case "InventoryDescription":
                    inventoryItems = sortDirection == "desc" ? inventoryItems.OrderByDescending(i => i.InventoryDescription) : inventoryItems.OrderBy(i => i.InventoryDescription);
                    break;
                case "InventoryQuantity":
                    inventoryItems = sortDirection == "desc" ? inventoryItems.OrderByDescending(i => i.InventoryQuantity) : inventoryItems.OrderBy(i => i.InventoryQuantity);
                    break;
                case "InventoryUnitType":
                    inventoryItems = sortDirection == "desc" ? inventoryItems.OrderByDescending(i => i.InventoryUnitType) : inventoryItems.OrderBy(i => i.InventoryUnitType);
                    break;
                case "InventoryPriceList":
                    inventoryItems = sortDirection == "desc" ? inventoryItems.OrderByDescending(i => i.InventoryPriceList) : inventoryItems.OrderBy(i => i.InventoryPriceList);
                    break;
                case "MaterialCategory":
                    inventoryItems = sortDirection == "desc" ? inventoryItems.OrderByDescending(i => i.MaterialCategory.CategoryName) : inventoryItems.OrderBy(i => i.MaterialCategory.CategoryName);
                    break;
                default:
                    inventoryItems = inventoryItems.OrderBy(i => i.InventoryCode); // Default sorting by InventoryCode
                    break;
            }

            ViewData["CodeSort"] = sortField == "InventoryCode" ? (sortDirection == "asc" ? "desc" : "") : "asc";
            ViewData["DescriptionSort"] = sortField == "InventoryDescription" ? (sortDirection == "asc" ? "desc" : "") : "asc";
            ViewData["QuantitySort"] = sortField == "InventoryQuantity" ? (sortDirection == "asc" ? "desc" : "") : "asc";
            ViewData["UnitTypeSort"] = sortField == "InventoryUnitType" ? (sortDirection == "asc" ? "desc" : "") : "asc";
            ViewData["PriceListSort"] = sortField == "InventoryPriceList" ? (sortDirection == "asc" ? "desc" : "") : "asc";
            ViewData["CategorySort"] = sortField == "MaterialCategory" ? (sortDirection == "asc" ? "desc" : "") : "asc";
            ViewData["sortDirection"] = sortDirection;
            ViewData["sortField"] = sortField;

            int pageSize = 5; // Adjust as needed

            var pagedData = await PaginatedList<Inventory>.CreateAsync(inventoryItems.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }




        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventorys == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorys
                .Include(i => i.MaterialCategory)
                .FirstOrDefaultAsync(m => m.InventoryID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            ViewData["MaterialCategoryID"] = new SelectList(_context.MaterialCategorys, "MaterialCategoryID", "CategoryName");
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryID,InventoryCode,InventoryDescription,InventoryQuantity,InventoryUnitType,InventoryPriceList,MaterialCategoryID")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialCategoryID"] = new SelectList(_context.MaterialCategorys, "MaterialCategoryID", "CategoryName", inventory.MaterialCategoryID);
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventorys == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorys.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["MaterialCategoryID"] = new SelectList(_context.MaterialCategorys, "MaterialCategoryID", "CategoryName", inventory.MaterialCategoryID);
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryID,InventoryCode,InventoryDescription,InventoryQuantity,InventoryUnitType,InventoryPriceList,MaterialCategoryID")] Inventory inventory)
        {
            if (id != inventory.InventoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryID))
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
            ViewData["MaterialCategoryID"] = new SelectList(_context.MaterialCategorys, "MaterialCategoryID", "CategoryName", inventory.MaterialCategoryID);
            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventorys == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorys
                .Include(i => i.MaterialCategory)
                .FirstOrDefaultAsync(m => m.InventoryID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventorys == null)
            {
                return Problem("Entity set 'NBDContext.Inventorys'  is null.");
            }
            var inventory = await _context.Inventorys.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventorys.Remove(inventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
          return _context.Inventorys.Any(e => e.InventoryID == id);
        }
    }
}
