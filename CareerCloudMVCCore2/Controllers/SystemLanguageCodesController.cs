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
    public class SystemLanguageCodesController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public SystemLanguageCodesController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: SystemLanguageCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemLanguageCodes.ToListAsync());
        }

        // GET: SystemLanguageCodes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLanguageCodes = await _context.SystemLanguageCodes
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (systemLanguageCodes == null)
            {
                return NotFound();
            }

            return View(systemLanguageCodes);
        }

        // GET: SystemLanguageCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemLanguageCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,Name,NativeName")] SystemLanguageCodes systemLanguageCodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemLanguageCodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemLanguageCodes);
        }

        // GET: SystemLanguageCodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLanguageCodes = await _context.SystemLanguageCodes.FindAsync(id);
            if (systemLanguageCodes == null)
            {
                return NotFound();
            }
            return View(systemLanguageCodes);
        }

        // POST: SystemLanguageCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LanguageId,Name,NativeName")] SystemLanguageCodes systemLanguageCodes)
        {
            if (id != systemLanguageCodes.LanguageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemLanguageCodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemLanguageCodesExists(systemLanguageCodes.LanguageId))
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
            return View(systemLanguageCodes);
        }

        // GET: SystemLanguageCodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemLanguageCodes = await _context.SystemLanguageCodes
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (systemLanguageCodes == null)
            {
                return NotFound();
            }

            return View(systemLanguageCodes);
        }

        // POST: SystemLanguageCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var systemLanguageCodes = await _context.SystemLanguageCodes.FindAsync(id);
            _context.SystemLanguageCodes.Remove(systemLanguageCodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemLanguageCodesExists(string id)
        {
            return _context.SystemLanguageCodes.Any(e => e.LanguageId == id);
        }
    }
}
