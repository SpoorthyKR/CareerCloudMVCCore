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
    public class SecurityLoginsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public SecurityLoginsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: SecurityLogins
        public async Task<IActionResult> Index()
        {
            return View(await _context.SecurityLogins.ToListAsync());
        }

        // GET: SecurityLogins/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLogins = await _context.SecurityLogins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLogins == null)
            {
                return NotFound();
            }

            return View(securityLogins);
        }

        // GET: SecurityLogins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecurityLogins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,CreatedDate,PasswordUpdateDate,AgreementAcceptedDate,IsLocked,IsInactive,EmailAddress,PhoneNumber,FullName,ForceChangePassword,PrefferredLanguage,TimeStamp")] SecurityLogins securityLogins)
        {
            if (ModelState.IsValid)
            {
                securityLogins.Id = Guid.NewGuid();
                _context.Add(securityLogins);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Create), "ApplicantProfiles", new { id = securityLogins.Id });
            }
            return View(securityLogins);
        }

        // GET: SecurityLogins/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLogins = await _context.SecurityLogins.FindAsync(id);
            if (securityLogins == null)
            {
                return NotFound();
            }
            return View(securityLogins);
        }

        // POST: SecurityLogins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,Password,CreatedDate,PasswordUpdateDate,AgreementAcceptedDate,IsLocked,IsInactive,EmailAddress,PhoneNumber,FullName,ForceChangePassword,PrefferredLanguage,TimeStamp")] SecurityLogins securityLogins)
        {
            if (id != securityLogins.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLogins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginsExists(securityLogins.Id))
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
            return View(securityLogins);
        }

        // GET: SecurityLogins/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLogins = await _context.SecurityLogins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLogins == null)
            {
                return NotFound();
            }

            return View(securityLogins);
        }

        // POST: SecurityLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLogins = await _context.SecurityLogins.FindAsync(id);
            _context.SecurityLogins.Remove(securityLogins);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginsExists(Guid id)
        {
            return _context.SecurityLogins.Any(e => e.Id == id);
        }
    }
}
