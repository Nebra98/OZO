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
    public class OpremaController : Controller
    {
        private readonly PI06Context _context;

        public OpremaController(PI06Context context)
        {
            _context = context;
        }
        public ActionResult CreateDocumentWithoutFilter()
        {
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            PdfFont bigFont = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            PdfBrush brush = new PdfSolidBrush(Syncfusion.Drawing.Color.Black);
            PdfDocument doc = new PdfDocument();
            //Add a page to the document.
            PdfPage page = doc.Pages.Add();
            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;
            PdfGrid pdfGrid = new PdfGrid();

            //Load the image as stream.
            FileStream imageStream = new FileStream("logo.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            graphics.DrawImage(image, 200, 200);

            //Draw the image
            Syncfusion.Drawing.RectangleF bounds = new Syncfusion.Drawing.RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);
            PdfCompositeField compField = new PdfCompositeField(bigFont, brush, "OZO - Izvjestaj o opremi");

            compField.Draw(header.Graphics, new Syncfusion.Drawing.PointF(120, 0));

            //Add the header at the top.

            doc.Template.Top = header;
            //Save the PDF document to stream
            //Create a Page template that can be used as footer.


            PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds);



            //Create page number field.

            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

            //Create page count field.

            PdfPageCountField count = new PdfPageCountField(font, brush);

            //Add the fields in composite fields.

            PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Stranica {0} od {1}", pageNumber, count);

            compositeField.Bounds = footer.Bounds;

            //Draw the composite field in footer.

            compositeField.Draw(footer.Graphics, new Syncfusion.Drawing.PointF(450, 40));

            //Add the footer template at the bottom.

            doc.Template.Bottom = footer;

            MemoryStream stream = new MemoryStream();
            List<object> data = new List<object>();

            var oprema = _context.Oprema
                             .AsNoTracking()
                             .OrderBy(o => o.Id)
                             .ToList();
            foreach (var komad in oprema)
            {
                var row = new
                {
                    Naziv = komad.Naziv,
                    Status = komad.Status,
                    Dostupnost = komad.Dostupnost
                };
                data.Add(row);


            }







            //Add list to IEnumerable
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(50, 50));
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "Oprema-Reports.pdf";

            return File(stream, contentType, fileName);


        }
        public void ispisi(List<Oprema> oprema, List<object> data)
        {
            foreach (var komad in oprema)
            {
                var row = new
                {
                    Naziv = komad.Naziv,
                    Status = komad.Status,
                    Dostupnost = komad.Dostupnost
                };
                data.Add(row);


            }


        }
        public ActionResult CreateDocument(string id, string vrsta, string znak)
        {

            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            PdfFont bigFont = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            PdfBrush brush = new PdfSolidBrush(Syncfusion.Drawing.Color.Black);
            PdfDocument doc = new PdfDocument();
            //Add a page to the document.
            PdfPage page = doc.Pages.Add();
            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;
            PdfGrid pdfGrid = new PdfGrid();

            //Load the image as stream.
            FileStream imageStream = new FileStream("logo.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            graphics.DrawImage(image, 200, 200);

            //Draw the image
            Syncfusion.Drawing.RectangleF bounds = new Syncfusion.Drawing.RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);
            PdfCompositeField compField = new PdfCompositeField(bigFont, brush, "OZO - Izvjestaj o natjecajima");

            compField.Draw(header.Graphics, new Syncfusion.Drawing.PointF(120, 0));

            //Add the header at the top.

            doc.Template.Top = header;
            //Save the PDF document to stream
            //Create a Page template that can be used as footer.


            PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds);



            //Create page number field.

            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

            //Create page count field.

            PdfPageCountField count = new PdfPageCountField(font, brush);

            //Add the fields in composite fields.

            PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Stranica {0} od {1}", pageNumber, count);

            compositeField.Bounds = footer.Bounds;

            //Draw the composite field in footer.

            compositeField.Draw(footer.Graphics, new Syncfusion.Drawing.PointF(450, 40));

            //Add the footer template at the bottom.

            doc.Template.Bottom = footer;

            MemoryStream stream = new MemoryStream();
            //Add values to list
            List<object> data = new List<object>();

            switch (vrsta)
            {

                case "Naziv":

                    var oprema = _context.Oprema
                                  .AsNoTracking()
                                  .Where(n => n.Naziv.Equals(id))
                                  .OrderBy(o => o.Id)
                                  .ToList();
                    ispisi(oprema, data);

                    break;
                
                case "Status":
                    oprema = _context.Oprema
                                  .AsNoTracking()
                                  .Where(n => n.Status.Equals(id))
                                  .OrderBy(o => o.Id)
                                  .ToList();
                    ispisi(oprema, data);


                    break;
                case "Dostupnost":
                    bool stanje = bool.Parse(id);
                    oprema = _context.Oprema
                                  .AsNoTracking()
                                  .Where(n => n.Dostupnost.Equals(id))
                                  .OrderBy(o => o.Id)
                                  .ToList();
                    ispisi(oprema, data);


                    break;
               
                default:
                    Console.WriteLine("Niste unijeli string");
                    break;
            }










            //Add list to IEnumerable
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(50, 50));

            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "Oprema-Reports.pdf";

            return File(stream, contentType, fileName);


        }
        // GET: Oprema
        public async Task<IActionResult> Index(string id)
        {
         
            var oprema = from n in _context.Oprema.Include(o => o.IdReferentniTipNavigation)
            select n;

            if (!String.IsNullOrEmpty(id))
            {
                oprema = oprema.Where(s => s.Naziv.Contains(id));
            }


            return View(await oprema.ToListAsync());
        }
        
        public void ispisi(List<Natječaj> natječaji, List<object> data)
        {
            foreach (var natječaj in natječaji)
            {
                var row = new { Naziv = natječaj.Naziv, Opis = natječaj.Opis, Cijena = natječaj.Cijena, Poslodavac = natječaj.Poslodavac, Status = natječaj.Status, Lokacija = natječaj.Lokacija, Kontakt = natječaj.Kontakt };
                data.Add(row);



            }

        }
        
        // GET: Oprema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema
                .Include(o => o.IdReferentniTipNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }

        // GET: Oprema/Create
        public IActionResult Create()
        {
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv");
            return View();
        }

        // POST: Oprema/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Status,Dostupnost,IdReferentniTip")] Oprema oprema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oprema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", oprema.IdReferentniTip);
            return View(oprema);
        }

        // GET: Oprema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema.FindAsync(id);
            if (oprema == null)
            {
                return NotFound();
            }
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", oprema.IdReferentniTip);
            return View(oprema);
        }

        // POST: Oprema/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Status,Dostupnost,IdReferentniTip")] Oprema oprema)
        {
            if (id != oprema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oprema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpremaExists(oprema.Id))
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
            ViewData["IdReferentniTip"] = new SelectList(_context.ReferentniTip, "Id", "Naziv", oprema.IdReferentniTip);
            return View(oprema);
        }

        // GET: Oprema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema
                .Include(o => o.IdReferentniTipNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }

        // POST: Oprema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oprema = await _context.Oprema.FindAsync(id);
            _context.Oprema.Remove(oprema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpremaExists(int id)
        {
            return _context.Oprema.Any(e => e.Id == id);
        }
    }
}
