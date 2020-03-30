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
    public class ApplicantProfilesController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public ApplicantProfilesController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: ApplicantProfiles
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.ApplicantProfiles
                .Include(a => a.CountryCodeNavigation)
                .Include(a => a.LoginNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: ApplicantProfiles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfiles = await _context.ApplicantProfiles
                .Include(a => a.CountryCodeNavigation)
                .Include(a => a.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantProfiles == null)
            {
                return NotFound();
            }

            return View(applicantProfiles);
        }

        // GET: ApplicantProfiles/Create
        public IActionResult Create(Guid Id)
        {
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code");
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "Login");
            return View();
        }

        // POST: ApplicantProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrentSalary,CurrentRate,Currency,CountryCode,StateProvinceCode,StreetAddress,CityTown,ZipPostalCode")] ApplicantProfiles applicantProfiles,Guid id)
        {
            applicantProfiles.Login = id;
           
            if (ModelState.IsValid)
            {
                applicantProfiles.Id = Guid.NewGuid();
                _context.Add(applicantProfiles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantProfiles.CountryCode);
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "Login", applicantProfiles.Login);
            return View(applicantProfiles);
        }

        // GET: ApplicantProfiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfiles = await _context.ApplicantProfiles.FindAsync(id);
            if (applicantProfiles == null)
            {
                return NotFound();
            }
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantProfiles.CountryCode);
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", applicantProfiles.Login);
            return View(applicantProfiles);
        }

        // POST: ApplicantProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,CurrentSalary,CurrentRate,Currency,CountryCode,StateProvinceCode,StreetAddress,CityTown,ZipPostalCode,TimeStamp")] ApplicantProfiles applicantProfiles)
        {
            if (id != applicantProfiles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantProfiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantProfilesExists(applicantProfiles.Id))
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
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantProfiles.CountryCode);
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", applicantProfiles.Login);
            return View(applicantProfiles);
        }

        // GET: ApplicantProfiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfiles = await _context.ApplicantProfiles
                .Include(a => a.CountryCodeNavigation)
                .Include(a => a.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantProfiles == null)
            {
                return NotFound();
            }

            return View(applicantProfiles);
        }

        // POST: ApplicantProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantProfiles = await _context.ApplicantProfiles.FindAsync(id);
            _context.ApplicantProfiles.Remove(applicantProfiles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantProfilesExists(Guid id)
        {
            return _context.ApplicantProfiles.Any(e => e.Id == id);
        }
    }
}
