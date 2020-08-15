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
    public class CertifikatZaposlenikController : Controller
    {
        private readonly PI06Context _context;

        public CertifikatZaposlenikController(PI06Context context)
        {
            _context = context;
        }

        // GET: CertifikatZaposlenik
        public async Task<IActionResult> Index()
        {
            var pI06Context = _context.CertifikatZaposlenik.Include(c => c.IdCertifikatNavigation).Include(c => c.IdZaposlenikNavigation);
            return View(await pI06Context.ToListAsync());
        }

        // GET: CertifikatZaposlenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikatZaposlenik = await _context.CertifikatZaposlenik
                .Include(c => c.IdCertifikatNavigation)
                .Include(c => c.IdZaposlenikNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certifikatZaposlenik == null)
            {
                return NotFound();
            }

            return View(certifikatZaposlenik);
        }

        // GET: CertifikatZaposlenik/Create
        public IActionResult Create()
        {
            ViewData["IdCertifikat"] = new SelectList(_context.Certifikat, "Id", "Naziv");
            ViewData["IdZaposlenik"] = new SelectList(_context.Zaposlenik, "Id", "Ime");
            return View();
        }

        // POST: CertifikatZaposlenik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCertifikat,IdZaposlenik")] CertifikatZaposlenik certifikatZaposlenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certifikatZaposlenik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCertifikat"] = new SelectList(_context.Certifikat, "Id", "Naziv", certifikatZaposlenik.IdCertifikat);
            ViewData["IdZaposlenik"] = new SelectList(_context.Zaposlenik, "Id", "Ime", certifikatZaposlenik.IdZaposlenik);
            return View(certifikatZaposlenik);
        }

        // GET: CertifikatZaposlenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikatZaposlenik = await _context.CertifikatZaposlenik.FindAsync(id);
            if (certifikatZaposlenik == null)
            {
                return NotFound();
            }
            ViewData["IdCertifikat"] = new SelectList(_context.Certifikat, "Id", "Naziv", certifikatZaposlenik.IdCertifikat);
            ViewData["IdZaposlenik"] = new SelectList(_context.Zaposlenik, "Id", "Ime", certifikatZaposlenik.IdZaposlenik);
            return View(certifikatZaposlenik);
        }

        // POST: CertifikatZaposlenik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCertifikat,IdZaposlenik")] CertifikatZaposlenik certifikatZaposlenik)
        {
            if (id != certifikatZaposlenik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certifikatZaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertifikatZaposlenikExists(certifikatZaposlenik.Id))
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
            ViewData["IdCertifikat"] = new SelectList(_context.Certifikat, "Id", "Naziv", certifikatZaposlenik.IdCertifikat);
            ViewData["IdZaposlenik"] = new SelectList(_context.Zaposlenik, "Id", "Ime", certifikatZaposlenik.IdZaposlenik);
            return View(certifikatZaposlenik);
        }

        // GET: CertifikatZaposlenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikatZaposlenik = await _context.CertifikatZaposlenik
                .Include(c => c.IdCertifikatNavigation)
                .Include(c => c.IdZaposlenikNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certifikatZaposlenik == null)
            {
                return NotFound();
            }

            return View(certifikatZaposlenik);
        }

        // POST: CertifikatZaposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certifikatZaposlenik = await _context.CertifikatZaposlenik.FindAsync(id);
            _context.CertifikatZaposlenik.Remove(certifikatZaposlenik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertifikatZaposlenikExists(int id)
        {
            return _context.CertifikatZaposlenik.Any(e => e.Id == id);
        }
    }
}
