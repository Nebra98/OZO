using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OZO.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Grid;
using System.Security.Principal;

namespace OZO.Controllers
{
    public class NatjecajController : Controller
    {
        private readonly PI06Context _context;

        public NatjecajController(PI06Context context)
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
            List<object> data = new List<object>();
            
                var natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .OrderBy(o => o.Id)
                                 .ToList();
                foreach (var natječaj in natječaji)
                {
                    var row = new { Naziv = natječaj.Naziv, Opis = natječaj.Opis, Cijena = natječaj.Cijena, Poslodavac = natječaj.Poslodavac, Status = natječaj.Status, Lokacija = natječaj.Lokacija, Kontakt = natječaj.Kontakt };
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
            string fileName = "Natječaji-Reports.pdf";
            
            return File(stream, contentType, fileName);


        }
        public void ispisi(List<Natječaj> natječaji, List<object> data)
        {
            Console.WriteLine("AAA");
            foreach (var natječaj in natječaji)
            {
                var row = new { Naziv = natječaj.Naziv, Opis = natječaj.Opis, Cijena = natječaj.Cijena, Poslodavac = natječaj.Poslodavac, Status = natječaj.Status, Lokacija = natječaj.Lokacija, Kontakt = natječaj.Kontakt };
                data.Add(row);



            }

        }
        public ActionResult CreateDocument(string id, string vrsta,string znak)
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
            PdfCompositeField compField = new PdfCompositeField(bigFont, brush,"OZO - Izvjestaj o natjecajima");

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

                   var natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .Where(n => n.Naziv.Equals(id))
                                 .OrderBy(o => o.Id)
                                 .ToList();
                    ispisi(natječaji, data);

                    break;
                case "Cijena":
                    int cijena = int.Parse(id);

                    if (znak == "manje")
                    {
                         natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .Where(n => n.Cijena < cijena)
                                 .OrderBy(o => o.Id)
                                 .ToList();
                        ispisi(natječaji, data);

                    }
                    else if (znak == "vise")
                    {
                         natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .Where(n => n.Cijena > cijena)
                                 .OrderBy(o => o.Id)
                                 .ToList();
                        ispisi(natječaji, data);

                    }
                    else
                    {
                        natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .Where(n => n.Cijena.Equals(cijena))
                                 .OrderBy(o => o.Id)
                                 .ToList();
                        ispisi(natječaji, data);

                    }
                    break;
                case "Poslodavac":
                    natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .Where(n => n.Poslodavac.Equals(id))
                                 .OrderBy(o => o.Id)
                                 .ToList();
                    ispisi(natječaji, data);

                    break;
                case "Status":
                    bool stanje = bool.Parse(id);
                    natječaji = _context.Natječaj
                                 .AsNoTracking()
                                 .Where(n => n.Status.Equals(stanje))
                                 .OrderBy(o => o.Id)
                                 .ToList();
                    ispisi(natječaji, data);

                    break;
                case "Lokacija":
                    natječaji = _context.Natječaj
                                     .AsNoTracking()
                                     .Where(n => n.Lokacija.Equals(id))
                                     .OrderBy(o => o.Id)
                                     .ToList();
                    ispisi(natječaji, data);

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
            string fileName = "Natječaji-Reports.pdf";

            return File(stream, contentType, fileName);


        }
        // GET: Natjecaj
        public async Task<IActionResult> Index(string id)
        {
            var natječaji = from n in _context.Natječaj
                         select n;

            if (!String.IsNullOrEmpty(id))
            {
                natječaji = natječaji.Where(s => s.Naziv.Contains(id));
            }
           
           
            return View(await natječaji.ToListAsync());
        }
        

        private IQueryable<T> IQueryable<T>(List<T> lists)
        {
            throw new NotImplementedException();
        }

        // GET: Natjecaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natječaj = await _context.Natječaj
                .FirstOrDefaultAsync(m => m.Id == id);
            if (natječaj == null)
            {
                return NotFound();
            }

            return View(natječaj);
        }

        // GET: Natjecaj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Natjecaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis,Cijena,Poslodavac,Status,Lokacija,Kontakt")] Natječaj natječaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(natječaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(natječaj);
        }

        // GET: Natjecaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natječaj = await _context.Natječaj.FindAsync(id);
            if (natječaj == null)
            {
                return NotFound();
            }
            return View(natječaj);
        }

        // POST: Natjecaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis,Cijena,Poslodavac,Status,Lokacija,Kontakt")] Natječaj natječaj)
        {
            if (id != natječaj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(natječaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NatječajExists(natječaj.Id))
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
            return View(natječaj);
        }

        // GET: Natjecaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natječaj = await _context.Natječaj
                .FirstOrDefaultAsync(m => m.Id == id);
            if (natječaj == null)
            {
                return NotFound();
            }

            return View(natječaj);
        }

        // POST: Natjecaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var natječaj = await _context.Natječaj.FindAsync(id);
            _context.Natječaj.Remove(natječaj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NatječajExists(int id)
        {
            return _context.Natječaj.Any(e => e.Id == id);
        }
    }
}
