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
    public class CompanyJobEducationsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public CompanyJobEducationsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: CompanyJobEducations
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.CompanyJobEducations.Include(c => c.JobNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: CompanyJobEducations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducations = await _context.CompanyJobEducations
                .Include(c => c.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobEducations == null)
            {
                return NotFound();
            }

            return View(companyJobEducations);
        }

        // GET: CompanyJobEducations/Create
        public IActionResult Create(Guid id)
        {
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: CompanyJobEducations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Major,Importance")] CompanyJobEducations companyJobEducations,Guid id)
        {
            companyJobEducations.Job = id;
            if (ModelState.IsValid)
            {
                companyJobEducations.Id = Guid.NewGuid();
                _context.Add(companyJobEducations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"CompanyProfiles");
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobEducations.Job);
            return View(companyJobEducations);
        }

        // GET: CompanyJobEducations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducations = await _context.CompanyJobEducations.FindAsync(id);
            if (companyJobEducations == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobEducations.Job);
            return View(companyJobEducations);
        }

        // POST: CompanyJobEducations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,Major,Importance,TimeStamp")] CompanyJobEducations companyJobEducations)
        {
            if (id != companyJobEducations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobEducations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobEducationsExists(companyJobEducations.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobEducations.Job);
            return View(companyJobEducations);
        }

        // GET: CompanyJobEducations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducations = await _context.CompanyJobEducations
                .Include(c => c.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobEducations == null)
            {
                return NotFound();
            }

            return View(companyJobEducations);
        }

        // POST: CompanyJobEducations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobEducations = await _context.CompanyJobEducations.FindAsync(id);
            _context.CompanyJobEducations.Remove(companyJobEducations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobEducationsExists(Guid id)
        {
            return _context.CompanyJobEducations.Any(e => e.Id == id);
        }
    }
}
