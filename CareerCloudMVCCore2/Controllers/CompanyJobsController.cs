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
    public class CompanyJobsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public CompanyJobsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: CompanyJobs
        public async Task<IActionResult> Index(Guid Applicant)
        {
            var jOB_PORTAL_DBContext = _context.CompanyJobs
                .Include(a=>a.CompanyJobsDescriptions)
                .Include(a=>a.CompanyJobEducations)
                .Include(a=>a.CompanyJobSkills)
                .Include(c => c.CompanyNavigation)
                   .ThenInclude(a=>a.CompanyDescriptions);
            ViewData["Applicant"] = Applicant;
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: CompanyJobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobs = await _context.CompanyJobs
                .Include(a=>a.CompanyJobsDescriptions)
                .Include(a=>a.CompanyJobEducations)
                .Include(a=>a.CompanyJobSkills)
                .Include(a=>a.ApplicantJobApplications)
                  .ThenInclude(a=>a.ApplicantNavigation)
                    .ThenInclude(a=>a.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobs == null)
            {
                return NotFound();
            }

            return View(companyJobs);
        }

        // GET: CompanyJobs/Create
        public IActionResult Create(Guid id)
        {
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone");
            return View();
        }

        // POST: CompanyJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobs companyJobs,Guid id)
        {
            companyJobs.Company = id;
            if (ModelState.IsValid)
            {
                companyJobs.Id = Guid.NewGuid();
                _context.Add(companyJobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create),"CompanyJobsDescriptions",new {id=companyJobs.Id});
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyJobs.Company);
            return View(companyJobs);
        }

        // GET: CompanyJobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobs = await _context.CompanyJobs.FindAsync(id);
            if (companyJobs == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyJobs.Company);
            return View(companyJobs);
        }

        // POST: CompanyJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden,TimeStamp")] CompanyJobs companyJobs)
        {
            if (id != companyJobs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobsExists(companyJobs.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyJobs.Company);
            return View(companyJobs);
        }

        // GET: CompanyJobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobs = await _context.CompanyJobs
                .Include(c => c.CompanyNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobs == null)
            {
                return NotFound();
            }

            return View(companyJobs);
        }

        // POST: CompanyJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobs = await _context.CompanyJobs.FindAsync(id);
            _context.CompanyJobs.Remove(companyJobs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ForApplicantDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobs = await _context.CompanyJobs
                .Include(a=>a.CompanyNavigation)
                .Include(a => a.CompanyJobsDescriptions)
                .Include(a => a.CompanyJobEducations)
                .Include(a => a.CompanyJobSkills)
                .Include(a => a.ApplicantJobApplications)
                  .ThenInclude(a => a.ApplicantNavigation)
                    .ThenInclude(a => a.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobs == null)
            {
                return NotFound();
            }

            return View(companyJobs);
        }
        private bool CompanyJobsExists(Guid id)
        {
            return _context.CompanyJobs.Any(e => e.Id == id);
        }
    }
}
