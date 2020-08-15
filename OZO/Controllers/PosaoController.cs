using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OZO.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace OZO.Controllers
{
    public class PosaoController : Controller
    {
        private readonly PI06Context _context;

        public PosaoController(PI06Context context)
        {
            _context = context;
        }

        // GET: Posao
        public async Task<IActionResult> Index(string id)
        {
            var usluge = from n in _context.Posao.Include(n => n.IdNatjecajNavigation).Include(u => u.IdUslugaNavigation)
                         select n;

            if (!String.IsNullOrEmpty(id))
            {
                usluge = usluge.Where(s => s.Naziv.Contains(id));
            }


            return View(await usluge.ToListAsync());
        }
        
        // GET: Posao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posao = await _context.Posao
                .Include(p => p.IdNatjecajNavigation)
                .Include(p => p.IdUslugaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posao == null)
            {
                return NotFound();
            }

            return View(posao);
        }

        // GET: Posao/Create
        public IActionResult Create()
        {
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv");
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv");
            return View();
        }

        // POST: Posao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis,Cijena,Klijent,Lokacija,Kontakt,IdUsluga,IdNatjecaj")] Posao posao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv", posao.IdNatjecaj);
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv", posao.IdUsluga);
            return View(posao);
        }

        // GET: Posao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posao = await _context.Posao.FindAsync(id);
            if (posao == null)
            {
                return NotFound();
            }
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv", posao.IdNatjecaj);
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv", posao.IdUsluga);
            return View(posao);
        }

        // POST: Posao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis,Cijena,Klijent,Lokacija,Kontakt,IdUsluga,IdNatjecaj")] Posao posao)
        {
            if (id != posao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosaoExists(posao.Id))
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
            ViewData["IdNatjecaj"] = new SelectList(_context.Natječaj, "Id", "Naziv", posao.IdNatjecaj);
            ViewData["IdUsluga"] = new SelectList(_context.Usluga, "Id", "Naziv", posao.IdUsluga);
            return View(posao);
        }

        // GET: Posao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posao = await _context.Posao
                .Include(p => p.IdNatjecajNavigation)
                .Include(p => p.IdUslugaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posao == null)
            {
                return NotFound();
            }

            return View(posao);
        }

        // POST: Posao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posao = await _context.Posao.FindAsync(id);
            _context.Posao.Remove(posao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosaoExists(int id)
        {
            return _context.Posao.Any(e => e.Id == id);
        }
    }
}
