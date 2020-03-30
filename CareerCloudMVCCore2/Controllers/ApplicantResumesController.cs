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
    public class ApplicantResumesController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public ApplicantResumesController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: ApplicantResumes
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.ApplicantResumes.Include(a => a.ApplicantNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: ApplicantResumes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumes = await _context.ApplicantResumes
                .Include(a => a.ApplicantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumes == null)
            {
                return NotFound();
            }

            return View(applicantResumes);
        }

        // GET: ApplicantResumes/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id");
            return View();
        }

        // POST: ApplicantResumes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Resume,LastUpdated")] ApplicantResumes applicantResumes)
        {
            if (ModelState.IsValid)
            {
                applicantResumes.Id = Guid.NewGuid();
                _context.Add(applicantResumes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantResumes.Applicant);
            return View(applicantResumes);
        }

        // GET: ApplicantResumes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumes = await _context.ApplicantResumes.FindAsync(id);
            if (applicantResumes == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantResumes.Applicant);
            return View(applicantResumes);
        }

        // POST: ApplicantResumes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Resume,LastUpdated")] ApplicantResumes applicantResumes)
        {
            if (id != applicantResumes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantResumes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantResumesExists(applicantResumes.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantResumes.Applicant);
            return View(applicantResumes);
        }

        // GET: ApplicantResumes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumes = await _context.ApplicantResumes
                .Include(a => a.ApplicantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumes == null)
            {
                return NotFound();
            }

            return View(applicantResumes);
        }

        // POST: ApplicantResumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantResumes = await _context.ApplicantResumes.FindAsync(id);
            _context.ApplicantResumes.Remove(applicantResumes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantResumesExists(Guid id)
        {
            return _context.ApplicantResumes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Resume(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumes = await _context.ApplicantResumes
                .Where(a => a.Applicant == id)
                .FirstOrDefaultAsync();
            if (applicantResumes == null)
            {
                return NotFound();
            }

            return View(applicantResumes);
        }
    }
}
