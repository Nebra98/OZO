using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OZO.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace OZO.Controllers
{
    public class CertifikatController : Controller
    {
        private readonly PI06Context _context;

        public CertifikatController(PI06Context context)
        {
            _context = context;
        }

        // GET: Certifikat
        public async Task<IActionResult> Index(string id)
        {

            var oprema = from n in _context.Certifikat
                         select n;

            if (!String.IsNullOrEmpty(id))
            {
                oprema = oprema.Where(s => s.Naziv.Contains(id));
            }


            return View(await oprema.ToListAsync());
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
            PdfCompositeField compField = new PdfCompositeField(bigFont, brush, "OZO - Izvjestaj o certifikatima");

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

            var oprema = _context.Certifikat
                             .AsNoTracking()
                             .OrderBy(o => o.Id)
                             .ToList();
            foreach (var komad in oprema)
            {
                var row = new
                {
                    Naziv = komad.Naziv,

                };
                data.Add(row);


            }







            //Add list to IEnumerable
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page,new Syncfusion.Drawing.PointF(50, 50));
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "Certifikati-Reports.pdf";

            return File(stream, contentType, fileName);


        }


        // GET: Certifikat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikat = await _context.Certifikat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certifikat == null)
            {
                return NotFound();
            }

            return View(certifikat);
        }

        // GET: Certifikat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certifikat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] Certifikat certifikat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certifikat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certifikat);
        }

        // GET: Certifikat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikat = await _context.Certifikat.FindAsync(id);
            if (certifikat == null)
            {
                return NotFound();
            }
            return View(certifikat);
        }

        // POST: Certifikat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] Certifikat certifikat)
        {
            if (id != certifikat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certifikat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertifikatExists(certifikat.Id))
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
            return View(certifikat);
        }

        // GET: Certifikat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikat = await _context.Certifikat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certifikat == null)
            {
                return NotFound();
            }

            return View(certifikat);
        }

        // POST: Certifikat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certifikat = await _context.Certifikat.FindAsync(id);
            _context.Certifikat.Remove(certifikat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertifikatExists(int id)
        {
            return _context.Certifikat.Any(e => e.Id == id);
        }
    }
}
