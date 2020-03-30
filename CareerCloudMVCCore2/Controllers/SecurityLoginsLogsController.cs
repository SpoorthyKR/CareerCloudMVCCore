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
    public class SecurityLoginsLogsController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public SecurityLoginsLogsController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: SecurityLoginsLogs
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.SecurityLoginsLog.Include(s => s.LoginNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: SecurityLoginsLogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsLog = await _context.SecurityLoginsLog
                .Include(s => s.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsLog == null)
            {
                return NotFound();
            }

            return View(securityLoginsLog);
        }

        // GET: SecurityLoginsLogs/Create
        public IActionResult Create()
        {
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress");
            return View();
        }

        // POST: SecurityLoginsLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,SourceIp,LogonDate,IsSuccesful")] SecurityLoginsLog securityLoginsLog)
        {
            if (ModelState.IsValid)
            {
                securityLoginsLog.Id = Guid.NewGuid();
                _context.Add(securityLoginsLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsLog.Login);
            return View(securityLoginsLog);
        }

        // GET: SecurityLoginsLogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsLog = await _context.SecurityLoginsLog.FindAsync(id);
            if (securityLoginsLog == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsLog.Login);
            return View(securityLoginsLog);
        }

        // POST: SecurityLoginsLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,SourceIp,LogonDate,IsSuccesful")] SecurityLoginsLog securityLoginsLog)
        {
            if (id != securityLoginsLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLoginsLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginsLogExists(securityLoginsLog.Id))
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
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsLog.Login);
            return View(securityLoginsLog);
        }

        // GET: SecurityLoginsLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsLog = await _context.SecurityLoginsLog
                .Include(s => s.LoginNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsLog == null)
            {
                return NotFound();
            }

            return View(securityLoginsLog);
        }

        // POST: SecurityLoginsLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLoginsLog = await _context.SecurityLoginsLog.FindAsync(id);
            _context.SecurityLoginsLog.Remove(securityLoginsLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginsLogExists(Guid id)
        {
            return _context.SecurityLoginsLog.Any(e => e.Id == id);
        }
    }
}
