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
    public class UslugaController : Controller
    {
        private readonly PI06Context _context;

        public UslugaController(PI06Context context)
        {
            _context = context;
        }

        // GET: Usluga
        public async Task<IActionResult> Index(string id)
        {
            var usluge = from n in _context.Usluga
                            select n;

            if (!String.IsNullOrEmpty(id))
            {
                usluge = usluge.Where(s => s.Naziv.Contains(id));
            }


            return View(await usluge.ToListAsync());
        }
        public ActionResult CreateDocumentWithoutFilter(string id)
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
            PdfCompositeField compField = new PdfCompositeField(bigFont, brush, "OZO - Izvjestaj o uslugama");

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
           
            
                var usluge = _context.Usluga
                                 .AsNoTracking()
                                 .OrderBy(o => o.Id)
                                 .ToList();
            
               foreach (var usluga in usluge)
                {
                    var row = new { Naziv = usluga.Naziv, Opis = usluga.Opis, Cijena = usluga.Cijena, Klijent = usluga.Klijent, Lokacija = usluga.Lokacija, Kontakt = usluga.Kontakt };
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
            string fileName = "Usluge-Reports.pdf";

            return File(stream, contentType, fileName);


        }
        public void ispisi(List<Usluga> usluge, List<object> data)
        {
            foreach (var usluga in usluge)
            {
                var row = new { Naziv = usluga.Naziv, Opis = usluga.Opis, Cijena = usluga.Cijena, Klijent = usluga.Klijent, Lokacija = usluga.Lokacija, Kontakt = usluga.Kontakt };
                data.Add(row);



            }

        }
        public ActionResult CreateDocument(string id, string vrsta, string znak)
        {
            Console.WriteLine(znak);
            Console.WriteLine(vrsta);
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
            PdfCompositeField compField = new PdfCompositeField(bigFont, brush, "OZO - Izvjestaj o uslugama");

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

                    var usluge = _context.Usluga
                                  .AsNoTracking()
                                  .Where(n => n.Naziv.Equals(id))
                                  .OrderBy(o => o.Id)
                                  .ToList();
                    ispisi(usluge, data);

                    break;
                case "Cijena":
                    int cijena = int.Parse(id);

                    if (znak == "manje")
                    {
                        usluge = _context.Usluga
                                .AsNoTracking()
                                .Where(n => n.Cijena < cijena)
                                .OrderBy(o => o.Id)
                                .ToList();
                        ispisi(usluge, data);

                    }
                    else if (znak == "vise")
                    {
                        usluge = _context.Usluga
                                .AsNoTracking()
                                .Where(n => n.Cijena > cijena)
                                .OrderBy(o => o.Id)
                                .ToList();
                        ispisi(usluge, data);

                    }
                    else
                    {
                        usluge = _context.Usluga
                                .AsNoTracking()
                                .Where(n => n.Cijena.Equals(cijena))
                                .OrderBy(o => o.Id)
                                .ToList();
                        ispisi(usluge, data);

                    }
                    break;
                case "Klijent":
                    usluge = _context.Usluga
                               .AsNoTracking()
                               .Where(n => n.Cijena.Equals(id))
                               .OrderBy(o => o.Id)
                               .ToList();
                    ispisi(usluge, data);

                    break;
                
                case "Lokacija":
                    usluge = _context.Usluga
                               .AsNoTracking()
                               .Where(n => n.Cijena.Equals(id))
                               .OrderBy(o => o.Id)
                               .ToList();
                    ispisi(usluge, data);

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
            string fileName = "Usluge-Reports.pdf";

            return File(stream, contentType, fileName);


        }

        // GET: Usluga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // GET: Usluga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usluga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis,Cijena,Klijent,Lokacija,Kontakt")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usluga);
        }

        // GET: Usluga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga.FindAsync(id);
            if (usluga == null)
            {
                return NotFound();
            }
            return View(usluga);
        }

        // POST: Usluga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis,Cijena,Klijent,Lokacija,Kontakt")] Usluga usluga)
        {
            if (id != usluga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaExists(usluga.Id))
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
            return View(usluga);
        }

        // GET: Usluga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // POST: Usluga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usluga = await _context.Usluga.FindAsync(id);
            _context.Usluga.Remove(usluga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaExists(int id)
        {
            return _context.Usluga.Any(e => e.Id == id);
        }
    }
}
