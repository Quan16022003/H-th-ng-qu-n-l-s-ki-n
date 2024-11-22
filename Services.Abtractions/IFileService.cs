using Microsoft.AspNetCore.Http;

namespace Services.Abtractions
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        Task DeleteFileAsync(string filePath);
    }
}