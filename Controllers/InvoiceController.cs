using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Services;


namespace StudentApp.Controllers
{
    
    public class InvoiceController : Controller
    {
        private readonly PdfService _pdfService;
        public InvoiceController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }
        [HttpPost("generate")]
        public IActionResult GenerateInvoice([FromBody] Invoice invoice1)
        {
            if (invoice1 == null)
            {
                return BadRequest("Invalid invoice data.");
            }

            // Generate PDF bytes
            var pdfBytes = _pdfService.GenerateInvoicePdf(invoice1);

            // Return the PDF file as a downloadable file
            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }
        public IActionResult Index()
        {
            // Mock invoice data for demonstration
            var invoice = new Invoice
            {
                InvoiceNumber = 1001,
                InvoiceDate = DateTime.Today,
                CustomerName = "John Doe",
                Amount = 500.00m
            };

            return View(invoice); // Pass invoice object to the view
        }
        [HttpPost]
        public IActionResult GeneratePdf([FromBody] Invoice invoice)
        {
            
            if (invoice == null)
            {
                return BadRequest("Invalid invoice data.");
            }

            // Generate PDF bytes using PdfService
            var pdfBytes = _pdfService.GenerateInvoicePdf(invoice);

            // Return the PDF file as a downloadable file
            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
