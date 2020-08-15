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
    public class PosaoOpremaController : Controller
    {
        private readonly PI06Context _context;

        public PosaoOpremaController(PI06Context context)
        {
            _context = context;
        }

        // GET: PosaoOprema
        public async Task<IActionResult> Index()
        {
            var pI06Context = _context.PosaoOprema.Include(p => p.IdOpremaNavigation).Include(p => p.IdPosaoNavigation);
            return View(await pI06Context.ToListAsync());
        }

        // GET: PosaoOprema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posaoOprema = await _context.PosaoOprema
                .Include(p => p.IdOpremaNavigation)
                .Include(p => p.IdPosaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posaoOprema == null)
            {
                return NotFound();
            }

            return View(posaoOprema);
        }

        // GET: PosaoOprema/Create
        public IActionResult Create()
        {
            ViewData["IdOprema"] = new SelectList(_context.Oprema, "Id", "Naziv");
            ViewData["IdPosao"] = new SelectList(_context.Posao, "Id", "Naziv");
            return View();
        }

        // POST: PosaoOprema/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPosao,IdOprema")] PosaoOprema posaoOprema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posaoOprema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOprema"] = new SelectList(_context.Oprema, "Id", "Naziv", posaoOprema.IdOprema);
            ViewData["IdPosao"] = new SelectList(_context.Posao, "Id", "Naziv", posaoOprema.IdPosao);
            return View(posaoOprema);
        }

        // GET: PosaoOprema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posaoOprema = await _context.PosaoOprema.FindAsync(id);
            if (posaoOprema == null)
            {
                return NotFound();
            }
            ViewData["IdOprema"] = new SelectList(_context.Oprema, "Id", "Naziv", posaoOprema.IdOprema);
            ViewData["IdPosao"] = new SelectList(_context.Posao, "Id", "Naziv", posaoOprema.IdPosao);
            return View(posaoOprema);
        }

        // POST: PosaoOprema/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPosao,IdOprema")] PosaoOprema posaoOprema)
        {
            if (id != posaoOprema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posaoOprema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosaoOpremaExists(posaoOprema.Id))
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
            ViewData["IdOprema"] = new SelectList(_context.Oprema, "Id", "Naziv", posaoOprema.IdOprema);
            ViewData["IdPosao"] = new SelectList(_context.Posao, "Id", "Naziv", posaoOprema.IdPosao);
            return View(posaoOprema);
        }

        // GET: PosaoOprema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posaoOprema = await _context.PosaoOprema
                .Include(p => p.IdOpremaNavigation)
                .Include(p => p.IdPosaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posaoOprema == null)
            {
                return NotFound();
            }

            return View(posaoOprema);
        }

        // POST: PosaoOprema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posaoOprema = await _context.PosaoOprema.FindAsync(id);
            _context.PosaoOprema.Remove(posaoOprema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosaoOpremaExists(int id)
        {
            return _context.PosaoOprema.Any(e => e.Id == id);
        }
    }
}
