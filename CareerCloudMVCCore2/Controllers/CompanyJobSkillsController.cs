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
    public class CompanyJobSkillsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public CompanyJobSkillsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: CompanyJobSkills
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.CompanyJobSkills.Include(c => c.JobNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: CompanyJobSkills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobSkills = await _context.CompanyJobSkills
                .Include(c => c.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobSkills == null)
            {
                return NotFound();
            }

            return View(companyJobSkills);
        }

        // GET: CompanyJobSkills/Create
        public IActionResult Create(Guid guid)
        {
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: CompanyJobSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Skill,SkillLevel,Importance")] CompanyJobSkills companyJobSkills,Guid id)
        {
            companyJobSkills.Job = id;
            if (ModelState.IsValid)
            {
                companyJobSkills.Id = Guid.NewGuid();
                _context.Add(companyJobSkills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create),"CompanyJobEducations", new {id=companyJobSkills.Job});
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobSkills.Job);
            return View(companyJobSkills);
        }

        // GET: CompanyJobSkills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobSkills = await _context.CompanyJobSkills.FindAsync(id);
            if (companyJobSkills == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobSkills.Job);
            return View(companyJobSkills);
        }

        // POST: CompanyJobSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,Skill,SkillLevel,Importance,TimeStamp")] CompanyJobSkills companyJobSkills)
        {
            if (id != companyJobSkills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobSkills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobSkillsExists(companyJobSkills.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobSkills.Job);
            return View(companyJobSkills);
        }

        // GET: CompanyJobSkills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobSkills = await _context.CompanyJobSkills
                .Include(c => c.JobNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobSkills == null)
            {
                return NotFound();
            }

            return View(companyJobSkills);
        }

        // POST: CompanyJobSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobSkills = await _context.CompanyJobSkills.FindAsync(id);
            _context.CompanyJobSkills.Remove(companyJobSkills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobSkillsExists(Guid id)
        {
            return _context.CompanyJobSkills.Any(e => e.Id == id);
        }
    }
}
