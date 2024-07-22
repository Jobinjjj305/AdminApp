// Services/PdfService.cs
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using StudentApp.Models;
namespace StudentApp.Services
{
    public class PdfService
    {
        public byte[] GenerateInvoicePdf(Invoice invoice)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add content to the PDF
                Paragraph header = new Paragraph("Invoice");
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                // Add invoice details
                document.Add(new Paragraph($"Invoice Number: {invoice.InvoiceNumber}"));
                document.Add(new Paragraph($"Invoice Date: {invoice.InvoiceDate.ToShortDateString()}"));
                document.Add(new Paragraph($"Customer Name: {invoice.CustomerName}"));
                document.Add(new Paragraph($"Amount: {invoice.Amount}"));

                document.Close();
                writer.Close();

                return ms.ToArray();
            }
        }
    }
}
