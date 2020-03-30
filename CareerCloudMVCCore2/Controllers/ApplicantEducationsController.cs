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
    public class ApplicantEducationsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public ApplicantEducationsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: ApplicantEducations
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.ApplicantEducations.Include(a => a.ApplicantNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: ApplicantEducations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducations = await _context.ApplicantEducations
                .Include(a => a.ApplicantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantEducations == null)
            {
                return NotFound();
            }

            return View(applicantEducations);
        }

        // GET: ApplicantEducations/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id");
            return View();
        }

        // POST: ApplicantEducations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent,TimeStamp")] ApplicantEducations applicantEducations)
        {
            if (ModelState.IsValid)
            {
                applicantEducations.Id = Guid.NewGuid();
                _context.Add(applicantEducations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantEducations.Applicant);
            return View(applicantEducations);
        }

        // GET: ApplicantEducations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducations = await _context.ApplicantEducations.FindAsync(id);
            if (applicantEducations == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantEducations.Applicant);
            return View(applicantEducations);
        }

        // POST: ApplicantEducations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent,TimeStamp")] ApplicantEducations applicantEducations)
        {
            if (id != applicantEducations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantEducations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantEducationsExists(applicantEducations.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantEducations.Applicant);
            return View(applicantEducations);
        }

        // GET: ApplicantEducations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducations = await _context.ApplicantEducations
                .Include(a => a.ApplicantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantEducations == null)
            {
                return NotFound();
            }

            return View(applicantEducations);
        }

        // POST: ApplicantEducations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantEducations = await _context.ApplicantEducations.FindAsync(id);
            _context.ApplicantEducations.Remove(applicantEducations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantEducationsExists(Guid id)
        {
            return _context.ApplicantEducations.Any(e => e.Id == id);
        }

        public async Task<IActionResult> EducationDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantEducations = await _context.ApplicantEducations
                .Where(m => m.Applicant == id)
                .FirstOrDefaultAsync();
            if (applicantEducations == null)
            {
                return NotFound();
            }

            return View(applicantEducations);
        }
    }
}
