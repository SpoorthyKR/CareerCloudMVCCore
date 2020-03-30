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
    public class SystemCountryCodesController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public SystemCountryCodesController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: SystemCountryCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemCountryCodes.ToListAsync());
        }

        // GET: SystemCountryCodes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCountryCodes = await _context.SystemCountryCodes
                .FirstOrDefaultAsync(m => m.Code == id);
            if (systemCountryCodes == null)
            {
                return NotFound();
            }

            return View(systemCountryCodes);
        }

        // GET: SystemCountryCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemCountryCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name")] SystemCountryCodes systemCountryCodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemCountryCodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemCountryCodes);
        }

        // GET: SystemCountryCodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCountryCodes = await _context.SystemCountryCodes.FindAsync(id);
            if (systemCountryCodes == null)
            {
                return NotFound();
            }
            return View(systemCountryCodes);
        }

        // POST: SystemCountryCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name")] SystemCountryCodes systemCountryCodes)
        {
            if (id != systemCountryCodes.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemCountryCodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemCountryCodesExists(systemCountryCodes.Code))
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
            return View(systemCountryCodes);
        }

        // GET: SystemCountryCodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCountryCodes = await _context.SystemCountryCodes
                .FirstOrDefaultAsync(m => m.Code == id);
            if (systemCountryCodes == null)
            {
                return NotFound();
            }

            return View(systemCountryCodes);
        }

        // POST: SystemCountryCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var systemCountryCodes = await _context.SystemCountryCodes.FindAsync(id);
            _context.SystemCountryCodes.Remove(systemCountryCodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemCountryCodesExists(string id)
        {
            return _context.SystemCountryCodes.Any(e => e.Code == id);
        }
    }
}
