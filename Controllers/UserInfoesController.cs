using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomSupport.Models;

namespace CustomSupport.Controllers
{
    public class UserInfoesController : Controller
    {
        private readonly LoggerdbContext _context;

        public UserInfoesController(LoggerdbContext context)
        {
            _context = context;
        }

        // GET: UserInfoes
        public async Task<IActionResult> Index()
        {
              return _context.UserInfos != null ? 
                          View(await _context.UserInfos.ToListAsync()) :
                          Problem("Entity set 'LoggerdbContext.UserInfos'  is null.");
        }

        // GET: UserInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserInfos == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfos
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email,Password")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserInfos == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfos.FindAsync(id);
            if (userInfo == null)
            {
                return NotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,Password")] UserInfo userInfo)
        {
            if (id != userInfo.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInfoExists(userInfo.UserId))
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
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInfos == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfos
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInfos == null)
            {
                return Problem("Entity set 'LoggerdbContext.UserInfos'  is null.");
            }
            var userInfo = await _context.UserInfos.FindAsync(id);
            if (userInfo != null)
            {
                _context.UserInfos.Remove(userInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInfoExists(int id)
        {
          return (_context.UserInfos?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
