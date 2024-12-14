using Constracts.DTO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http.Internal;
using Services.Abtractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services
{
    public class PDFService : IPDFService
    {
        private readonly IFileService _fileService;

        public PDFService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<string> GenerateTicketPdfAsync(AttendeePDFDTO attendeePDFDTO)
        {
            // Tạo tên file PDF
            var fileName = $"Ticket_{attendeePDFDTO.FirstName}_{attendeePDFDTO.LastName}_{Guid.NewGuid()}.pdf";
            var folder = Path.Combine("wwwroot", "TicketPdf", attendeePDFDTO.EventId.ToString());

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception dirEx)
            {
                throw new Exception("Lỗi khi tạo thư mục để lưu file PDF", dirEx);
            }

            // Sử dụng MemoryStream để tạo PDF
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    // Tạo PDF trong MemoryStream
                    using (PdfWriter writer = new PdfWriter(memoryStream))
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);
                        document.Add(new Paragraph("Thông Tin Vé").SetFontSize(20));
                        document.Add(new Paragraph($"Họ và Tên: {attendeePDFDTO.FirstName} {attendeePDFDTO.LastName}"));
                        document.Add(new Paragraph($"Ticket ID: {attendeePDFDTO.TicketId}"));
                        document.Add(new Paragraph($"Order ID: {attendeePDFDTO.OrderId}"));
                        document.Add(new Paragraph($"Event ID: {attendeePDFDTO.EventId}"));
                        document.Close();
                    }

                    // Reset vị trí của MemoryStream để đọc lại
                    memoryStream.Position = 0;

                    // Tạo đối tượng FormFile từ MemoryStream
                    var formFile = new FormFile(memoryStream, 0, memoryStream.Length, null, fileName);

                    // Upload file PDF lên server
                    var relativePath = await _fileService.UploadFileAsync(formFile, folder);

                    // Trả về đường dẫn tương đối của file PDF
                    return relativePath;
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi tạo hoặc upload PDF
                    throw new Exception("Lỗi khi tạo hoặc upload file PDF", ex);
                }
            }
        }
    }
}