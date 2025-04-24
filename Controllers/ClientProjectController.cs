	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBD3.Controllers;
using NBD3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NBD3.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NBD3.Controllers
{
	[Authorize(Roles = "Admin, General Manager, Designer, Sales Assoc")]
	public class ClientProjectController : ElephantController
	{
		private readonly NBDContext _context;

		public ClientProjectController(NBDContext context)
		{
			_context = context;
		}

		// GET: ClientProject
		[Authorize(Roles = "Admin, General Manager, Designer, Sales Assoc")]
		public async Task<IActionResult> Index(int? ClientID, string searchString, string sortField, string sortDirection, DateOnly? startDateFilter, DateOnly? endDateFilter)
		{
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Client");

			if (!ClientID.HasValue)
			{
				return Redirect(ViewData["returnURL"].ToString());
			}

			// Initialize sorting parameters
			ViewBag.NameSortParam = string.IsNullOrEmpty(sortDirection) || sortField != "name" ? "name_desc" : "";
			ViewBag.ClientSortParam = sortField == "client" ? "client_desc" : "client";
			ViewBag.LocationSortParam = sortField == "location" ? "location_desc" : "location";
			ViewBag.StartDateSortParam = sortField == "start_date" ? "start_date_desc" : "start_date";
			ViewBag.EndDateSortParam = sortField == "end_date" ? "end_date_desc" : "end_date";

			var projectsQuery = _context.Projects
				.Include(p => p.Client)
				.Include(p => p.Location)
				.Where(project =>
					(string.IsNullOrEmpty(searchString) || project.ProjectName.ToLower().Contains(searchString.ToLower())) &&
					(!ClientID.HasValue || project.ClientId == ClientID.Value));

			// Apply date filters
			if (startDateFilter.HasValue)
			{
				DateOnly startDate = startDateFilter.Value;
				projectsQuery = projectsQuery.Where(project => project.ProjectStartDate >= startDate);
			}

			if (endDateFilter.HasValue)
			{
				DateOnly endDate = endDateFilter.Value;
				projectsQuery = projectsQuery.Where(project =>
					!project.ProjectEndDate.HasValue || project.ProjectEndDate.Value <= endDate);
			}

			// Apply sorting
			switch (sortField)
			{
				case "name":
					projectsQuery = sortDirection == "desc" ? projectsQuery.OrderByDescending(p => p.ProjectName) : projectsQuery.OrderBy(p => p.ProjectName);
					break;
				case "client":
					projectsQuery = sortDirection == "desc" ? projectsQuery.OrderByDescending(p => p.Client.ClientFirstName) : projectsQuery.OrderBy(p => p.Client.ClientFirstName);
					break;
				case "location":
					projectsQuery = sortDirection == "desc" ? projectsQuery.OrderByDescending(p => p.Location.LocationName) : projectsQuery.OrderBy(p => p.Location.LocationName);
					break;
				case "start_date":
					projectsQuery = sortDirection == "desc" ? projectsQuery.OrderByDescending(p => p.ProjectStartDate) : projectsQuery.OrderBy(p => p.ProjectStartDate);
					break;
				case "end_date":
					projectsQuery = sortDirection == "desc" ? projectsQuery.OrderByDescending(p => p.ProjectEndDate) : projectsQuery.OrderBy(p => p.ProjectEndDate);
					break;
				default:
					projectsQuery = projectsQuery.OrderBy(p => p.ProjectName);
					break;
			}

			var projects = await projectsQuery.ToListAsync();

			var client = await _context.Clients
				.Where(c => c.ClientId == ClientID.GetValueOrDefault())
				.AsNoTracking()
				.FirstOrDefaultAsync();
			ViewBag.Client = client;

			ViewBag.SearchString = searchString;
			ViewBag.StartDateFilter = startDateFilter;
			ViewBag.EndDateFilter = endDateFilter;

			return View(projects);
		}


		// GET: ClientProject/Details/5
		[Authorize(Roles = "Admin, General Manager, Designer, Sales Assoc")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Projects == null)
			{
				return NotFound();
			}

			var project = await _context.Projects

				.Include(p => p.Client)
				.Include(p => p.Location)
				//.Include(p => p.ProjectStatus)
								.AsNoTracking()
				.FirstOrDefaultAsync(m => m.ProjectId == id);

			if (project == null)
			{
				return NotFound();
			}




			return View(project);
		}

		[Authorize(Roles = "Admin, General Manager, Designer")]
		// GET: ClientProject/Create
		public IActionResult Add(int? ClientID, string ClientName)
		{
			if (!ClientID.HasValue)
			{
				return Redirect(ViewData["returnURL"].ToString());
			}
			ViewData["ContactFullName"] = ClientName;

			Project project = new Project()
			{
				ClientId = ClientID.GetValueOrDefault()
			};
			ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationName");

			// Set the return URL
			ViewData["returnURL"] = "/ClientProject/Index?ClientID=" + ClientID;

			return View("Add", project);
		}


		// POST: ClientProject/Add
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin, General Manager, Designer")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add([Bind("ProjectId,ProjectName,ProjectDescription,ProjectLocation,ProjectStartDate,ProjectEndDate,ClientId,LocationId,StatusID")] Project project, string CustomerName)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Add project to the context
					_context.Add(project);
					// Debugging: Output the ClientId to ensure it's correct
					Console.WriteLine("ClientId: " + project.ClientId);
					await _context.SaveChangesAsync();
					// Redirect back to the index page
					return RedirectToAction("Index", new { ClientID = project.ClientId });
				}
			}
			catch (Exception ex)
			{
				// Handle any exceptions
				ViewData["ErrorMessage"] = "Error adding project: " + ex.Message;
				return View(project);
			}
			ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationName", project.LocationId);

			ViewData["CustomerName"] = CustomerName;
			return View(project);
		}



		// GET: ClientProject/Edit/5
		[Authorize(Roles = "Admin, General Manager, Designer, Sales Assoc")]
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null || _context.Projects == null)
			{
				return NotFound();
			}

			var project = await _context.Projects.Include(p => p.Client).FirstOrDefaultAsync(c => c.ProjectId == id);
			if (project == null)
			{
				return NotFound();
			}
			ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationCityAddress", project.LocationId);
			ViewData["StatusID"] = new SelectList(_context.Statuss, "StatusID", "StatusName", project.ProjectStatus);
			return View(project);
		}

		// POST: ClientProject/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin, General Manager,Designer, Sales Assoc")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(int id)
		{
			var projectToUpdate = await _context.Projects.FindAsync(id);

			if (projectToUpdate == null)
			{
				return NotFound();
			}

			if (await TryUpdateModelAsync<Project>(
				projectToUpdate,
				"",
				p => p.ProjectName,
				p => p.ProjectDescription,
				p => p.ProjectStartDate,
				p => p.ProjectEndDate,
				p => p.LocationId,
				p => p.ProjectStatus))
			{
				try
				{
					await _context.SaveChangesAsync();
					return Redirect(ViewData["returnURL"].ToString());
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProjectExists(projectToUpdate.ProjectId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}

			ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationCityAddress", projectToUpdate.LocationId);
			ViewData["StatusID"] = new SelectList(_context.Statuss, "StatusID", "StatusName", projectToUpdate.ProjectStatus);
			return View(projectToUpdate);
		}

		// GET: ClientProject/Delete/5
		[Authorize(Roles = "Admin, General Manager")]
		public async Task<IActionResult> Remove(int? id)
		{
			if (id == null || _context.Projects == null)
			{
				return NotFound();
			}

			var project = await _context.Projects
				.Include(p => p.Client)
				.Include(p => p.Location)
				.Include(p => p.ProjectStatus)
				.FirstOrDefaultAsync(m => m.ProjectId == id);
			if (project == null)
			{
				return NotFound();
			}

			return View(project);
		}

		// POST: ClientProject/Delete/5
		[Authorize(Roles = "Admin, General Manager")]
		[HttpPost, ActionName("Remove")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveConfirmed(int id)
		{
			if (_context.Projects == null)
			{
				return Problem("Entity set 'NBDContext.Projects'  is null.");
			}
			var project = await _context.Projects.Include(c => c.Client).FirstOrDefaultAsync(c => c.ProjectId == id);
			if (project != null)
			{
				_context.Projects.Remove(project);
			}

			await _context.SaveChangesAsync();
			return Redirect(ViewData["returnURL"].ToString());
		}

		private bool ProjectExists(int id)
		{
			return _context.Projects.Any(e => e.ProjectId == id);
		}
	}
}
