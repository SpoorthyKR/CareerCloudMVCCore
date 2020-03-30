using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloudMVCCore2.Models;

namespace CareerCloudMVCCore2.Controllers
{
    public class CompanyLocationsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public CompanyLocationsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: CompanyLocations
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.CompanyLocations.Include(c => c.CompanyNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: CompanyLocations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyLocations = await _context.CompanyLocations
                .Include(c => c.CompanyNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyLocations == null)
            {
                return NotFound();
            }

            return View(companyLocations);
        }

        // GET: CompanyLocations/Create
        public IActionResult Create(Guid id)
        {
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone");
            return View();
        }

        // POST: CompanyLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryCode,StateProvinceCode,StreetAddress,CityTown,ZipPostalCode")] CompanyLocations companyLocations,Guid id)
        {
            companyLocations.Company = id;
            if (ModelState.IsValid)
            {
                companyLocations.Id = Guid.NewGuid();
                _context.Add(companyLocations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"CompanyProfiles");
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyLocations.Company);
            return View(companyLocations);
        }

        // GET: CompanyLocations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyLocations = await _context.CompanyLocations.FindAsync(id);
            if (companyLocations == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyLocations.Company);
            return View(companyLocations);
        }

        // POST: CompanyLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,CountryCode,StateProvinceCode,StreetAddress,CityTown,ZipPostalCode,TimeStamp")] CompanyLocations companyLocations)
        {
            if (id != companyLocations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyLocations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyLocationsExists(companyLocations.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyLocations.Company);
            return View(companyLocations);
        }

        // GET: CompanyLocations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyLocations = await _context.CompanyLocations
                .Include(c => c.CompanyNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyLocations == null)
            {
                return NotFound();
            }

            return View(companyLocations);
        }

        // POST: CompanyLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyLocations = await _context.CompanyLocations.FindAsync(id);
            _context.CompanyLocations.Remove(companyLocations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyLocationsExists(Guid id)
        {
            return _context.CompanyLocations.Any(e => e.Id == id);
        }
    }
}
