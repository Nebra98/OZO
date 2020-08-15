using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OZO.Models;

namespace OZO.Controllers
{
    public class UslugaReferentniTipController : Controller
    {
        private readonly PI06Context _context;

        public UslugaReferentniTipController(PI06Context context)
        {
            _context = context;
        }

        // GET: UslugaReferentniTip
        public async Task<IActionResult> Index()
        {
            var pI06Context = _context.UslugaReferentniTip.Include(u => u.IdReferentniTipNavigation).Include(u => u.IdUslugaNavigation);
            return View(await pI06Context.ToListAsync());
        }

        // GET: UslugaReferentniTip/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaReferentniTip = await _context.UslugaReferentniTip
                .Include(u => u.IdReferentniTipNavigation)
                .Include(u => u.IdUslugaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uslugaReferentniTip == null)
            {
                return NotFound();
            }

            return View(uslugaReferentniTip);
        }

        // GET: UslugaReferentniTip/Create
        public IActionResult Create()
        {
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv");
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv");
            return View();
        }

        // POST: UslugaReferentniTip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsluga,IdReferentniTip")] UslugaReferentniTip uslugaReferentniTip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uslugaReferentniTip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", uslugaReferentniTip.IdReferentniTip);
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv", uslugaReferentniTip.IdUsluga);
            return View(uslugaReferentniTip);
        }

        // GET: UslugaReferentniTip/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaReferentniTip = await _context.UslugaReferentniTip.FindAsync(id);
            if (uslugaReferentniTip == null)
            {
                return NotFound();
            }
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", uslugaReferentniTip.IdReferentniTip);
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv", uslugaReferentniTip.IdUsluga);
            return View(uslugaReferentniTip);
        }

        // POST: UslugaReferentniTip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsluga,IdReferentniTip")] UslugaReferentniTip uslugaReferentniTip)
        {
            if (id != uslugaReferentniTip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uslugaReferentniTip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaReferentniTipExists(uslugaReferentniTip.Id))
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
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", uslugaReferentniTip.IdReferentniTip);
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv", uslugaReferentniTip.IdUsluga);
            return View(uslugaReferentniTip);
        }

        // GET: UslugaReferentniTip/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaReferentniTip = await _context.UslugaReferentniTip
                .Include(u => u.IdReferentniTipNavigation)
                .Include(u => u.IdUslugaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uslugaReferentniTip == null)
            {
                return NotFound();
            }

            return View(uslugaReferentniTip);
        }

        // POST: UslugaReferentniTip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uslugaReferentniTip = await _context.UslugaReferentniTip.FindAsync(id);
            _context.UslugaReferentniTip.Remove(uslugaReferentniTip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaReferentniTipExists(int id)
        {
            return _context.UslugaReferentniTip.Any(e => e.Id == id);
        }
    }
}
