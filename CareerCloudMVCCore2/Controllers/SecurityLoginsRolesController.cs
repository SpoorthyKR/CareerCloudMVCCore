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
    public class SecurityLoginsRolesController : Controller
    {
        private readonly JOB_PORTAL_DBContext _context;

        public SecurityLoginsRolesController(JOB_PORTAL_DBContext context)
        {
            _context = context;
        }

        // GET: SecurityLoginsRoles
        public async Task<IActionResult> Index()
        {
            var jOB_PORTAL_DBContext = _context.SecurityLoginsRoles.Include(s => s.LoginNavigation).Include(s => s.RoleNavigation);
            return View(await jOB_PORTAL_DBContext.ToListAsync());
        }

        // GET: SecurityLoginsRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsRoles = await _context.SecurityLoginsRoles
                .Include(s => s.LoginNavigation)
                .Include(s => s.RoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsRoles == null)
            {
                return NotFound();
            }

            return View(securityLoginsRoles);
        }

        // GET: SecurityLoginsRoles/Create
        public IActionResult Create()
        {
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress");
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role");
            return View();
        }

        // POST: SecurityLoginsRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Role,TimeStamp")] SecurityLoginsRoles securityLoginsRoles)
        {
            if (ModelState.IsValid)
            {
                securityLoginsRoles.Id = Guid.NewGuid();
                _context.Add(securityLoginsRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsRoles.Login);
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role", securityLoginsRoles.Role);
            return View(securityLoginsRoles);
        }

        // GET: SecurityLoginsRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsRoles = await _context.SecurityLoginsRoles.FindAsync(id);
            if (securityLoginsRoles == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsRoles.Login);
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role", securityLoginsRoles.Role);
            return View(securityLoginsRoles);
        }

        // POST: SecurityLoginsRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,Role,TimeStamp")] SecurityLoginsRoles securityLoginsRoles)
        {
            if (id != securityLoginsRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLoginsRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginsRolesExists(securityLoginsRoles.Id))
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
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsRoles.Login);
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role", securityLoginsRoles.Role);
            return View(securityLoginsRoles);
        }

        // GET: SecurityLoginsRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsRoles = await _context.SecurityLoginsRoles
                .Include(s => s.LoginNavigation)
                .Include(s => s.RoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsRoles == null)
            {
                return NotFound();
            }

            return View(securityLoginsRoles);
        }

        // POST: SecurityLoginsRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLoginsRoles = await _context.SecurityLoginsRoles.FindAsync(id);
            _context.SecurityLoginsRoles.Remove(securityLoginsRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginsRolesExists(Guid id)
        {
            return _context.SecurityLoginsRoles.Any(e => e.Id == id);
        }
    }
}
