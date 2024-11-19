using Microsoft.AspNetCore.Http;
using Services.Abtractions;

namespace Services
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;

        public FileService(string rootPath)
        {
            _rootPath = rootPath;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File không hợp lệ");
            }

            // Tạo đường dẫn thư mục lưu trữ
            var uploadPath = Path.Combine(_rootPath, folder);
            
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Tạo tên file duy nhất để tránh trùng lặp
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Lưu file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối của file
            return Path.Combine(folder, fileName).Replace("\\", "/");
        }

        public async Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Đường dẫn file không hợp lệ");
            }

            var fullPath = Path.Combine(_rootPath, filePath);

            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }
    }
}
