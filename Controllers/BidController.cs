using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NBD3.Data;
using NBD3.Models;

namespace NBD3.Controllers
{
    [Authorize(Roles = "Admin, General Manager, Designer, Sales Assoc")]
    public class BidController : Controller
    {
        private readonly NBDContext _context;

        public BidController(NBDContext context)
        {
            _context = context;
        }

        // GET: Bid
        [Authorize(Roles = "Admin, General Manager,Designer, Sales Assoc")]
        public async Task<IActionResult> Index(string projectFilter, string sortField, string sortDirection, int? pageSizeID, int? page)
        {
            var bids = _context.Bids
                .Include(b => b.Project)
                .Include(b => b.StaffDetails)
                .Include(b => b.LabourDetails)
                .Include(b => b.MaterialDetails)
                .AsQueryable(); // Ensure it's queryable

            // Apply filters and search
            if (!string.IsNullOrEmpty(projectFilter))
            {
                bids = bids.Where(b => b.Project.ProjectName.ToLower().Contains(projectFilter.ToLower()));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField)
                {
                    case "ProjectName":
                        bids = (sortDirection == "asc")
                            ? bids.OrderBy(b => b.Project.ProjectName)
                            : bids.OrderByDescending(b => b.Project.ProjectName);
                        break;
                    case "BidAmount":
                        bids = (sortDirection == "asc")
                            ? bids.OrderBy(b => b.BidAmount)
                            : bids.OrderByDescending(b => b.BidAmount);
                        break;
                    case "BidDate":
                        bids = (sortDirection == "asc")
                            ? bids.OrderBy(b => b.BidDate)
                            : bids.OrderByDescending(b => b.BidDate);
                        break;
                    case "BidCost":
                        bids = (sortDirection == "asc")
                            ? bids.OrderBy(b => b.BidCost)
                            : bids.OrderByDescending(b => b.BidCost);
                        break;
                    case "BidApprove":
                        bids = (sortDirection == "asc")
                            ? bids.OrderBy(b => b.BidApprove)
                            : bids.OrderByDescending(b => b.BidApprove);
                        break;
                    // Add more cases for other fields if needed
                    default:
                        // Default sorting logic, if none of the specified cases match
                        bids = bids.OrderBy(b => b.Project.ProjectName);
                        break;
                }
            }

            // Apply paging
            int pageSize = 5; // Adjust as needed
            var pagedBids = await PaginatedList<Bid>.CreateAsync(bids.AsNoTracking(), page ?? 1, pageSize);

            ViewData["projectFilter"] = projectFilter;
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            ViewData["pageSize"] = pageSize;

            return View(pagedBids);
        }

        // GET: Bid/Details/5
        [Authorize(Roles = "Admin, General Manager,Designer, Sales Assoc")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.StaffDetails)
                .Include(b => b.LabourDetails)
                .Include(b => b.MaterialDetails)
                .FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
            {
                return NotFound();
            }

            var StaffList = await _context.Staffs.ToListAsync();
            var staffNotListed = StaffList.Where(staff => !bid.StaffDetails.Any(st => st.StaffID == staff.StaffID)).ToList();
            ViewBag.Staffs = staffNotListed;

            var LabourList = await _context.Labours.ToListAsync();
            var LabourNotListed = LabourList.Where(labour => !bid.LabourDetails.Any(st => st.LabourID == labour.LabourID)).ToList();
            ViewBag.Labours = LabourNotListed;

            var MaterialList = await _context.Inventorys.ToListAsync();
            var MaterialNotListed = MaterialList.Where(labour => !bid.MaterialDetails.Any(st => st.InventoryID == labour.InventoryID)).ToList();
            ViewBag.Materials = MaterialNotListed;



            return View(bid);
        }

        // GET: Bid/Create
        [Authorize(Roles = "Admin, General Manager,Designer")]
        public IActionResult Create()
        {

            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");

            return View();
        }

        // POST: Bid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, General Manager,Designer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,BidAmount,BidDate,BidCost,BidApprove,BidRejectedManager,BidApproveClient,BidRejectedClient,BidStatus,BidNoteReason,ProjectID")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Details), new { id = bid.BidId });
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", bid.ProjectID);

            return View(bid);

        }

        // GET: Bid/Edit/5
        [Authorize(Roles = "Admin, General Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(c => c.Project)
                .Include(c => c.StaffDetails)
                .Include(c => c.LabourDetails)
                .Include(c => c.MaterialDetails)
                .FirstOrDefaultAsync(c => c.BidId == id);

            if (bid == null)
            {
                return NotFound();
            }

            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectId", "ProjectName", bid.ProjectID);
            ViewBag.Staffs = await _context.Staffs.ToListAsync();
            ViewBag.Materials = await _context.Inventorys.ToListAsync();
            ViewBag.Labours = await _context.Labours.ToListAsync();

            return View(bid);
        }

        // POST: Bid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, General Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,BidAmount,BidDate,BidCost,BidApprove,BidRejectedManager,BidApproveClient,BidRejectedClient,BidStatus,BidNoteReason,ProjectID")] Bid bid)
        {
            if (id != bid.BidId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.BidId))
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

            return View(bid);
        }

        // GET: Bid/Delete/5
        [Authorize(Roles = "Admin, General Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.StaffDetails)
                .Include(b => b.LabourDetails)
                .Include(b => b.MaterialDetails)
                .FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bid/Delete/5
        [Authorize(Roles = "Admin, General Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bids == null)
            {
                return Problem("Entity set 'NBDContext.Bids'  is null.");
            }
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Bid/Delete Staff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStaff(int bidId, int staffId)
        {
            var staff = await _context.Bids
                .Include(s => s.StaffDetails)
                .FirstOrDefaultAsync(s => s.BidId == bidId);

            if (staff == null)
            {
                return NotFound();
            }

            var staffDetail = staff.StaffDetails.FirstOrDefault(st => st.StaffID == staffId);
            if (staffDetail != null)
            {
                _context.StaffDetails.Remove(staffDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = bidId });
        }

        // POST: Bid/Delete Material
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMaterial(int bidId, int materialId)
        {
            var material = await _context.Bids
                .Include(m => m.MaterialDetails)
                .FirstOrDefaultAsync(m => m.BidId == bidId);

            if (material == null)
            {
                return NotFound();
            }

            var materialDetail = material.MaterialDetails.FirstOrDefault(st => st.InventoryID == materialId);
            if (materialDetail != null)
            {
                _context.MaterialDetails.Remove(materialDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = bidId });
        }

        // POST: Bid/Delete Labour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLabour(int bidId, int labourId)
        {
            var labour = await _context.Bids
                .Include(m => m.LabourDetails)
                .FirstOrDefaultAsync(m => m.BidId == bidId);

            if (labour == null)
            {
                return NotFound();
            }

            var labourDetail = labour.LabourDetails.FirstOrDefault(st => st.LabourID == labourId);
            if (labourDetail != null)
            {
                _context.LabourDetails.Remove(labourDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = bidId });
        }

        //adds
        // POST: Course/AddLabour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLabour(int bidId, int labourId, int quantity)
        {
            var bid = await _context.Bids
                .Include(c => c.LabourDetails)
                .FirstOrDefaultAsync(c => c.BidId == bidId);
            var labour = await _context.Labours.FindAsync(labourId);

            if (bid == null || labour == null)
            {
                return NotFound();
            }

            bid.AddLabour(labour, quantity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = bidId });
        }

        // POST: Course/AddMaterial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMaterial(int bidId, int inventoryId, int quantity)
        {
            var bid = await _context.Bids
                .Include(c => c.MaterialDetails)
                .FirstOrDefaultAsync(c => c.BidId == bidId);
            var material = await _context.Inventorys.FindAsync(inventoryId);

            if (bid == null || material == null)
            {
                return NotFound();
            }

            bid.AddMaterial(material, quantity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = bidId });
        }

        // POST: Course/Add Staff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStaff(int bidId, int staffId)
        {
            var bid = await _context.Bids
                .Include(c => c.StaffDetails)
                .FirstOrDefaultAsync(c => c.BidId == bidId);
            var staff = await _context.Staffs.FindAsync(staffId);

            if (bid == null)
            {
                return NotFound();
            }

            bid.AddStaff(staff);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = bidId });
        }

        public async Task<IActionResult> Copy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(c => c.Project)
                .Include(c => c.StaffDetails)
                .Include(c => c.LabourDetails)
                .Include(c => c.MaterialDetails)
                .FirstOrDefaultAsync(c => c.BidId == id);

            if (bid == null)
            {
                return NotFound();
            }
            var newbid = new Bid
            {
                BidAmount = bid.BidAmount,
                BidDate = DateOnly.FromDateTime(DateTime.Now),
                BidCost = bid.BidCost,
                BidApprove = false,
                BidRejectedManager = false,
                BidApproveClient = false,
                BidRejectedClient = false,
                BidStatus = BidStatusList.Copy.ToString(),
                BidNoteReason = "",
                ProjectID = bid.ProjectID,
            };

            _context.Bids.Add(newbid);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = newbid.BidId });

        }


        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}
