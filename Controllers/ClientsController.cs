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
    public class ClientsController : Controller
    {
        private readonly NBDContext _context;

        public ClientsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Clients
        [Authorize(Roles = "Admin, General Manager, Supervisor, Designer, Group Manager, Admin Assistant, Sales Assoc, Production Worker")]
        public async Task<IActionResult> Index(string companyFilter, string lastNameSearch, string sortField, string sortDirection, int? pageSizeID, int? page)
        {
            var clients = _context.Clients.AsQueryable();

            // Apply filters and search
            if (!string.IsNullOrEmpty(companyFilter))
            {
                clients = clients.Where(c => c.ClientCommpanyName.ToLower().Contains(companyFilter.ToLower()));
            }

            if (!string.IsNullOrEmpty(lastNameSearch))
            {
                clients = clients.Where(c => c.ClientLastName.ToLower().Contains(lastNameSearch.ToLower()));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField)
                {
                    case "ClientCommpanyName":
                        clients = (sortDirection == "asc")
                            ? clients.OrderBy(c => c.ClientCommpanyName)
                            : clients.OrderByDescending(c => c.ClientCommpanyName);
                        break;

                    // Sort by concatenation of ClientFirstName and ClientLastName for ContactFullName
                    case "ContactFullName":
                        if (sortDirection == "asc")
                        {
                            clients = clients.OrderBy(c => c.ClientFirstName)
                                             .ThenBy(c => c.ClientLastName);
                        }
                        else
                        {
                            clients = clients.OrderByDescending(c => c.ClientFirstName)
                                             .ThenByDescending(c => c.ClientLastName);
                        }
                        break;


                    default:
                        // Default sorting logic, if none of the specified cases match
                        clients = clients.OrderBy(c => c.ClientCommpanyName);
                        break;
                }
            }

            int pageSize = 3; // Adjust as needed
            var pagedClients = await PaginatedList<Client>.CreateAsync(clients.AsNoTracking(), page ?? 1, pageSize);

            // Populate ViewData for the filter, search, sort, and pagination values
            ViewData["companyFilter"] = companyFilter;
            ViewData["lastNameSearch"] = lastNameSearch;
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            return View(pagedClients);
        }



        // GET: Clients/Details/5
        [Authorize(Roles = "Admin, General Manager, Designer, Sales Assoc")] // Admin, General Manager, Supervisor, and Sales Assoc roles can access this action
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin, General Manager, Sales Assoc")]
        public IActionResult Create()
        {
            var client = new Client
            {
                ClientCountryAddress = "Canada"
            };
            return View(client);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, General Manager,Sales Assoc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientCommpanyName,ClientFirstName,ClientMiddleName,ClientLastName,ClientPhone,ClientEmail,ClientStreetAddress,ClientPostalCode,ClientCityAddress,ClientCountryAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin, General Manager,Sales Assoc")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, General Manager, Sales Assoc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientCommpanyName,ClientFirstName,ClientMiddleName,ClientLastName,ClientPhone,ClientEmail,ClientStreetAddress,ClientPostalCode,ClientCityAddress,ClientCountryAddress")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin, General Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [Authorize(Roles = "Admin, General Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("There are no Clients to delete.");
            }

            var client = await _context.Clients.FindAsync(id);
            try
            {
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                }

                // Redirect to a specific action (e.g., Index) instead of using ViewData["returnURL"]
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Client. Remember, you cannot delete a Client that has associated projects.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            return View(client);
        }




        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
        public ActionResult ClientSection()
        {
            return View();
        }


    }
}
