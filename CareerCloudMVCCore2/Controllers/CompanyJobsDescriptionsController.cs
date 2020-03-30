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
    public class CompanyJobsDescriptionsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public CompanyJobsDescriptionsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: CompanyJobsDescriptions
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.CompanyJobsDescriptions.Include(c => c.JobNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: CompanyJobsDescriptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobsDescriptions = await _context.CompanyJobsDescriptions
                .Include(c => c.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobsDescriptions == null)
            {
                return NotFound();
            }

            return View(companyJobsDescriptions);
        }

        // GET: CompanyJobsDescriptions/Create
        public IActionResult Create(Guid id)
        {
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: CompanyJobsDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobName,JobDescriptions")] CompanyJobsDescriptions companyJobsDescriptions,Guid id)
        {
            companyJobsDescriptions.Job = id;
            if (ModelState.IsValid)
            {
                companyJobsDescriptions.Id = Guid.NewGuid();
                _context.Add(companyJobsDescriptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create),"CompanyJobSkills", new {id=companyJobsDescriptions.Job});
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobsDescriptions.Job);
            return View(companyJobsDescriptions);
        }

        // GET: CompanyJobsDescriptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobsDescriptions = await _context.CompanyJobsDescriptions.FindAsync(id);
            if (companyJobsDescriptions == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobsDescriptions.Job);
            return View(companyJobsDescriptions);
        }

        // POST: CompanyJobsDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,JobName,JobDescriptions,TimeStamp")] CompanyJobsDescriptions companyJobsDescriptions)
        {
            if (id != companyJobsDescriptions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobsDescriptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobsDescriptionsExists(companyJobsDescriptions.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobsDescriptions.Job);
            return View(companyJobsDescriptions);
        }

        // GET: CompanyJobsDescriptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobsDescriptions = await _context.CompanyJobsDescriptions
                .Include(c => c.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobsDescriptions == null)
            {
                return NotFound();
            }

            return View(companyJobsDescriptions);
        }

        // POST: CompanyJobsDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobsDescriptions = await _context.CompanyJobsDescriptions.FindAsync(id);
            _context.CompanyJobsDescriptions.Remove(companyJobsDescriptions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        


        private bool CompanyJobsDescriptionsExists(Guid id)
        {
            return _context.CompanyJobsDescriptions.Any(e => e.Id == id);
        }
    }
}
