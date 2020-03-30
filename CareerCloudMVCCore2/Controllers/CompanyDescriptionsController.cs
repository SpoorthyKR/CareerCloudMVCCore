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
    public class CompanyDescriptionsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public CompanyDescriptionsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: CompanyDescriptions
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.CompanyDescriptions.Include(c => c.CompanyNavigation).Include(c => c.Language);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: CompanyDescriptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptions = await _context.CompanyDescriptions
                .Include(c => c.CompanyNavigation)
                .Include(c => c.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptions == null)
            {
                return NotFound();
            }

            return View(companyDescriptions);
        }

        // GET: CompanyDescriptions/Create
        public IActionResult Create(Guid id)
        {
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone");
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageId", "LanguageId");
            return View();
        }

        // POST: CompanyDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,CompanyName,CompanyDescription")] CompanyDescriptions companyDescriptions,Guid id)
        {
            companyDescriptions.Company = id;
            if (ModelState.IsValid)
            {
                companyDescriptions.Id = Guid.NewGuid();
                _context.Add(companyDescriptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create),"CompanyLocations",new { id = companyDescriptions.Company });
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyDescriptions.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageId", "LanguageId", companyDescriptions.LanguageId);
            return View(companyDescriptions);
        }

        // GET: CompanyDescriptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptions = await _context.CompanyDescriptions.FindAsync(id);
            if (companyDescriptions == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyDescriptions.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageId", "LanguageId", companyDescriptions.LanguageId);
            return View(companyDescriptions);
        }

        // POST: CompanyDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,LanguageId,CompanyName,CompanyDescription,TimeStamp")] CompanyDescriptions companyDescriptions)
        {
            if (id != companyDescriptions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyDescriptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDescriptionsExists(companyDescriptions.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "ContactPhone", companyDescriptions.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageId", "LanguageId", companyDescriptions.LanguageId);
            return View(companyDescriptions);
        }

        // GET: CompanyDescriptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptions = await _context.CompanyDescriptions
                .Include(c => c.CompanyNavigation)
                .Include(c => c.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptions == null)
            {
                return NotFound();
            }

            return View(companyDescriptions);
        }

        // POST: CompanyDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyDescriptions = await _context.CompanyDescriptions.FindAsync(id);
            _context.CompanyDescriptions.Remove(companyDescriptions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyDescriptionsExists(Guid id)
        {
            return _context.CompanyDescriptions.Any(e => e.Id == id);
        }
    }
}
