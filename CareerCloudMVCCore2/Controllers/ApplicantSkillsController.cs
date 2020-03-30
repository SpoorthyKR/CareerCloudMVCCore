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
    public class ApplicantSkillsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public ApplicantSkillsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: ApplicantSkills
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.ApplicantSkills.Include(a => a.ApplicantNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: ApplicantSkills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkills = await _context.ApplicantSkills
                .Include(a => a.ApplicantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSkills == null)
            {
                return NotFound();
            }

            return View(applicantSkills);
        }

        // GET: ApplicantSkills/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id");
            return View();
        }

        // POST: ApplicantSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear,TimeStamp")] ApplicantSkills applicantSkills)
        {
            if (ModelState.IsValid)
            {
                applicantSkills.Id = Guid.NewGuid();
                _context.Add(applicantSkills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantSkills.Applicant);
            return View(applicantSkills);
        }

        // GET: ApplicantSkills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkills = await _context.ApplicantSkills.FindAsync(id);
            if (applicantSkills == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantSkills.Applicant);
            return View(applicantSkills);
        }

        // POST: ApplicantSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear,TimeStamp")] ApplicantSkills applicantSkills)
        {
            if (id != applicantSkills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantSkills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantSkillsExists(applicantSkills.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantSkills.Applicant);
            return View(applicantSkills);
        }

        // GET: ApplicantSkills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkills = await _context.ApplicantSkills
                .Include(a => a.ApplicantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSkills == null)
            {
                return NotFound();
            }

            return View(applicantSkills);
        }

        // POST: ApplicantSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantSkills = await _context.ApplicantSkills.FindAsync(id);
            _context.ApplicantSkills.Remove(applicantSkills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantSkillsExists(Guid id)
        {
            return _context.ApplicantSkills.Any(e => e.Id == id);
        }

        public IActionResult Skills(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkills = _context.ApplicantSkills
                .Where(a => a.Applicant == id);



            if (applicantSkills == null)
            {
                return NotFound();
            }

            return View(applicantSkills.ToList());
        }
    }
}
