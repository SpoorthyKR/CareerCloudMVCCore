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
    public class ApplicantJobApplicationsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public ApplicantJobApplicationsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: ApplicantJobApplications
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.ApplicantJobApplications
                .Include(a => a.ApplicantNavigation)
                .Include(a => a.JobNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: ApplicantJobApplications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplications = await _context.ApplicantJobApplications
                .Include(a => a.ApplicantNavigation)
                .Include(a => a.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantJobApplications == null)
            {
                return NotFound();
            }

            return View(applicantJobApplications);
        }

        // GET: ApplicantJobApplications/Create
        public IActionResult Create(Guid Applicant,Guid Job)
        {
            //ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id");
            //ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            ViewData["Applicant"] = Applicant;
            ViewData["Job"] = Job;
            return View();
        }

        // POST: ApplicantJobApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicantJobApplications applicantJobApplications, Guid Applicant, Guid Job)
        {

            applicantJobApplications.Applicant = Applicant;
            applicantJobApplications.Job = Job;
            if (ModelState.IsValid)
            {
                applicantJobApplications.Id = Guid.NewGuid();
                _context.Add(applicantJobApplications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantJobApplications.Applicant);
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", applicantJobApplications.Job);
            return View(applicantJobApplications);
        }

        // GET: ApplicantJobApplications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplications = await _context.ApplicantJobApplications.FindAsync(id);
            if (applicantJobApplications == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantJobApplications.Applicant);
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", applicantJobApplications.Job);
            return View(applicantJobApplications);
        }

        // POST: ApplicantJobApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Job,ApplicationDate,TimeStamp")] ApplicantJobApplications applicantJobApplications)
        {
            if (id != applicantJobApplications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantJobApplications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantJobApplicationsExists(applicantJobApplications.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantJobApplications.Applicant);
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", applicantJobApplications.Job);
            return View(applicantJobApplications);
        }

        // GET: ApplicantJobApplications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplications = await _context.ApplicantJobApplications
                .Include(a => a.ApplicantNavigation)
                .Include(a => a.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantJobApplications == null)
            {
                return NotFound();
            }

            return View(applicantJobApplications);
        }

        // POST: ApplicantJobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantJobApplications = await _context.ApplicantJobApplications.FindAsync(id);
            _context.ApplicantJobApplications.Remove(applicantJobApplications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantJobApplicationsExists(Guid id)
        {
            return _context.ApplicantJobApplications.Any(e => e.Id == id);
        }

        public async Task<IActionResult> JobApplications(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplications = await _context.ApplicantJobApplications
                .Where(a => a.Applicant == id)
                .Include(a => a.JobNavigation)
                .ThenInclude(a => a.CompanyJobsDescriptions)
                .FirstOrDefaultAsync();


            if (applicantJobApplications == null)
            {
                return NotFound();
            }

            return View(applicantJobApplications.JobNavigation.CompanyJobsDescriptions);
        }
    }
}
