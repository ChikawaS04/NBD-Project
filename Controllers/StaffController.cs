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
    public class StaffController : Controller
    {
        private readonly NBDContext _context;

        public StaffController(NBDContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string firstNameSearch, string lastNameSearch, string titleSearch, string sortField, string sortDirection, int? page)
        {
            var staffs = _context.Staffs.AsQueryable();

            // Apply filters and search
            if (!string.IsNullOrEmpty(firstNameSearch))
            {
                string firstNameLower = firstNameSearch.ToLower();
                staffs = staffs.Where(s => s.StaffFirstName.ToLower().Contains(firstNameLower));
            }

            if (!string.IsNullOrEmpty(lastNameSearch))
            {
                string lastNameLower = lastNameSearch.ToLower();
                staffs = staffs.Where(s => s.StaffLastname.ToLower().Contains(lastNameLower));
            }

            if (!string.IsNullOrEmpty(titleSearch))
            {
                staffs = staffs.Where(s => s.StaffTitle.ToLower() == titleSearch.ToLower());
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortDirection))
            {
                switch (sortField.ToLower())
                {
                    case "stafftitle":
                        staffs = sortDirection.ToLower() == "asc" ? staffs.OrderBy(s => s.StaffTitle) : staffs.OrderByDescending(s => s.StaffTitle);
                        break;
                    case "stafffirstname":
                        staffs = sortDirection.ToLower() == "asc" ? staffs.OrderBy(s => s.StaffFirstName) : staffs.OrderByDescending(s => s.StaffFirstName);
                        break;
                    case "stafflastname":
                        staffs = sortDirection.ToLower() == "asc" ? staffs.OrderBy(s => s.StaffLastname) : staffs.OrderByDescending(s => s.StaffLastname);
                        break;
                    case "staffphone":
                        staffs = sortDirection.ToLower() == "asc" ? staffs.OrderBy(s => s.StaffPhone) : staffs.OrderByDescending(s => s.StaffPhone);
                        break;
                    case "staffemail":
                        staffs = sortDirection.ToLower() == "asc" ? staffs.OrderBy(s => s.StaffEmail) : staffs.OrderByDescending(s => s.StaffEmail);
                        break;
                    default:
                        break;
                }
            }

            // Paginate the results
            int pageSize = 5; // Default page size
            var pagedStaffs = await PaginatedList<Staff>.CreateAsync(staffs.AsNoTracking(), page ?? 1, pageSize);

            // Populate ViewData with pagination and sorting information
            ViewData["FirstNameSearch"] = firstNameSearch;
            ViewData["LastNameSearch"] = lastNameSearch;
            ViewData["TitleSearch"] = titleSearch;
            ViewData["SortField"] = sortField;
            ViewData["SortDirection"] = sortDirection;

            return View(pagedStaffs);
        }


        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(p => p.StaffPhoto)
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffID,StaffTitle,StaffFirstName,StaffLastname,StaffPhone,StaffEmail")] Staff staff, IFormFile pictureFile)
        {
            if (ModelState.IsValid)
            {
                await AddPicture(staff, pictureFile);
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffID,StaffTitle,StaffFirstName,StaffLastname,StaffPhone,StaffEmail")] Staff staff, string chkRemoveImage, IFormFile pictureFile)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (chkRemoveImage != null)
                    {
                        //If we are just deleting the two versions of the photo, we need to make sure the Change Tracker knows
                        //about them both so go get the Thumbnail since we did not include it.
                        staff.StaffThumbnail = _context.StaffThumbnails.Where(p => p.StaffID == staff.StaffID).FirstOrDefault();
                        //Then, setting them to null will cause them to be deleted from the database.
                        staff.StaffPhoto = null;
                        staff.StaffThumbnail = null;
                    }
                    else
                    {
                        await AddPicture(staff, pictureFile);
                    }
                    _context.Update(staff);
                    _context.Entry(staff).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffID))
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
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'NBDContext.Staffs'  is null.");
            }
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task AddPicture(Staff staff, IFormFile pictureFile)
        {
            //Get the picture and save it with the Patient (2 sizes)
            if (pictureFile != null)
            {
                string mimeType = pictureFile.ContentType;
                long fileLength = pictureFile.Length;
                if (!(mimeType == "" || fileLength == 0))//Looks like we have a file!!!
                {
                    if (mimeType.Contains("image"))
                    {
                        using var memoryStream = new MemoryStream();
                        await pictureFile.CopyToAsync(memoryStream);
                        var pictureArray = memoryStream.ToArray();//Gives us the Byte[]

                        //Check if we are replacing or creating new
                        if (staff.StaffPhoto != null)
                        {
                            //We already have pictures so just replace the Byte[]
                            staff.StaffPhoto.Content = ResizeImage.shrinkImageWebp(pictureArray, 500, 600);

                            //Get the Thumbnail so we can update it.  Remember we didn't include it
                            staff.StaffThumbnail = _context.StaffThumbnails.Where(p => p.StaffID == staff.StaffID).FirstOrDefault();
                            staff.StaffThumbnail.Content = ResizeImage.shrinkImageWebp(pictureArray, 75, 90);
                        }
                        else //No pictures saved so start new
                        {
                            staff.StaffPhoto = new StaffPhoto
                            {
                                Content = ResizeImage.shrinkImageWebp(pictureArray, 500, 600),
                                MimeType = "image/webp"
                            };
                            staff.StaffThumbnail = new StaffThumbnail
                            {
                                Content = ResizeImage.shrinkImageWebp(pictureArray, 75, 90),
                                MimeType = "image/webp"
                            };
                        }
                    }
                }
            }
        }

        private bool StaffExists(int id)
        {
          return _context.Staffs.Any(e => e.StaffID == id);
        }
    }
}
