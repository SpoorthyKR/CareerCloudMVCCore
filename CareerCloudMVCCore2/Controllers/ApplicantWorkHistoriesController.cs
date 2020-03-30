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
    public class ApplicantWorkHistoriesController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public ApplicantWorkHistoriesController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: ApplicantWorkHistories
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.ApplicantWorkHistory.Include(a => a.ApplicantNavigation).Include(a => a.CountryCodeNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: ApplicantWorkHistories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistory = await _context.ApplicantWorkHistory
                .Include(a => a.ApplicantNavigation)
                .Include(a => a.CountryCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistory == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistory);
        }

        // GET: ApplicantWorkHistories/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id");
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code");
            return View();
        }

        // POST: ApplicantWorkHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear,TimeStamp")] ApplicantWorkHistory applicantWorkHistory)
        {
            if (ModelState.IsValid)
            {
                applicantWorkHistory.Id = Guid.NewGuid();
                _context.Add(applicantWorkHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantWorkHistory.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantWorkHistory.CountryCode);
            return View(applicantWorkHistory);
        }

        // GET: ApplicantWorkHistories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistory = await _context.ApplicantWorkHistory.FindAsync(id);
            if (applicantWorkHistory == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantWorkHistory.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantWorkHistory.CountryCode);
            return View(applicantWorkHistory);
        }

        // POST: ApplicantWorkHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear,TimeStamp")] ApplicantWorkHistory applicantWorkHistory)
        {
            if (id != applicantWorkHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantWorkHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantWorkHistoryExists(applicantWorkHistory.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantWorkHistory.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantWorkHistory.CountryCode);
            return View(applicantWorkHistory);
        }

        // GET: ApplicantWorkHistories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistory = await _context.ApplicantWorkHistory
                .Include(a => a.ApplicantNavigation)
                .Include(a => a.CountryCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistory == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistory);
        }

        // POST: ApplicantWorkHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantWorkHistory = await _context.ApplicantWorkHistory.FindAsync(id);
            _context.ApplicantWorkHistory.Remove(applicantWorkHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantWorkHistoryExists(Guid id)
        {
            return _context.ApplicantWorkHistory.Any(e => e.Id == id);
        }

        public IActionResult WorkHistory(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistory = _context.ApplicantWorkHistory
                .Where(a => a.Applicant == id);

            if (applicantWorkHistory == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistory.ToList());
        }
    }
}
