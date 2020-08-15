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
    public class NatjecajReferentniTipController : Controller
    {
        private readonly PI06Context _context;

        public NatjecajReferentniTipController(PI06Context context)
        {
            _context = context;
        }

        // GET: NatjecajReferentniTip
        public async Task<IActionResult> Index()
        {
            var pI06Context = _context.NatjecajReferentniTip.Include(n => n.IdNatjecajNavigation).Include(n => n.IdReferentniTipNavigation);
            return View(await pI06Context.ToListAsync());
        }

        // GET: NatjecajReferentniTip/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natjecajReferentniTip = await _context.NatjecajReferentniTip
                .Include(n => n.IdNatjecajNavigation)
                .Include(n => n.IdReferentniTipNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (natjecajReferentniTip == null)
            {
                return NotFound();
            }

            return View(natjecajReferentniTip);
        }

        // GET: NatjecajReferentniTip/Create
        public IActionResult Create()
        {
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv");
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv");
            return View();
        }

        // POST: NatjecajReferentniTip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdNatjecaj,IdReferentniTip")] NatjecajReferentniTip natjecajReferentniTip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(natjecajReferentniTip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv", natjecajReferentniTip.IdNatjecaj);
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", natjecajReferentniTip.IdReferentniTip);
            return View(natjecajReferentniTip);
        }

        // GET: NatjecajReferentniTip/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natjecajReferentniTip = await _context.NatjecajReferentniTip.FindAsync(id);
            if (natjecajReferentniTip == null)
            {
                return NotFound();
            }
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv", natjecajReferentniTip.IdNatjecaj);
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", natjecajReferentniTip.IdReferentniTip);
            return View(natjecajReferentniTip);
        }

        // POST: NatjecajReferentniTip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdNatjecaj,IdReferentniTip")] NatjecajReferentniTip natjecajReferentniTip)
        {
            if (id != natjecajReferentniTip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(natjecajReferentniTip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NatjecajReferentniTipExists(natjecajReferentniTip.Id))
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
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv", natjecajReferentniTip.IdNatjecaj);
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", natjecajReferentniTip.IdReferentniTip);
            return View(natjecajReferentniTip);
        }

        // GET: NatjecajReferentniTip/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natjecajReferentniTip = await _context.NatjecajReferentniTip
                .Include(n => n.IdNatjecajNavigation)
                .Include(n => n.IdReferentniTipNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (natjecajReferentniTip == null)
            {
                return NotFound();
            }

            return View(natjecajReferentniTip);
        }

        // POST: NatjecajReferentniTip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var natjecajReferentniTip = await _context.NatjecajReferentniTip.FindAsync(id);
            _context.NatjecajReferentniTip.Remove(natjecajReferentniTip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NatjecajReferentniTipExists(int id)
        {
            return _context.NatjecajReferentniTip.Any(e => e.Id == id);
        }
    }
}
