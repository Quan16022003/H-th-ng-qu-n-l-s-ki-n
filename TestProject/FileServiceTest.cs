using Xunit;
using Moq;
using Services;
using Microsoft.AspNetCore.Http;
using System.Text;


namespace TestProject
{
    public class FileServiceTest : IDisposable
    {
        private readonly string _testRootPath;
        private readonly FileService _fileService;

        public FileServiceTest()
        {
            _testRootPath = Path.Combine(Path.GetTempPath(), $"TestFiles_{Guid.NewGuid()}");
            _fileService = new FileService(_testRootPath);
            
            Directory.CreateDirectory(_testRootPath);
        }

        [Fact]
        public async Task UploadFileAsync_ValidFile_ReturnsFilePath()
        {
            // Arrange
            var fileName = "test.txt";
            var content = "Test content";
            var folder = "uploads";

            var fileMock = new Mock<IFormFile>();
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(memoryStream.Length);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                   .Callback<Stream, CancellationToken>((stream, token) =>
                   {
                       memoryStream.CopyTo(stream);
                   })
                   .Returns(Task.CompletedTask);

            // Act
            var result = await _fileService.UploadFileAsync(fileMock.Object, folder);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(folder, result);
            Assert.True(File.Exists(Path.Combine(_testRootPath, result)));
        }
        [Fact]
        public async Task UploadFileAsync_NullFile_ThrowsArgumentException()
        {
            // Arrange
            IFormFile file = null;
            var folder = "uploads";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _fileService.UploadFileAsync(file, folder));
        }
        [Fact]
        public async Task DeleteFileAsync_ExistingFile_DeletesFile()
        {
            // Arrange
            var fileName = "test.txt";
            var folder = "uploads";
            var filePath = Path.Combine(folder, fileName);
            var fullPath = Path.Combine(_testRootPath, filePath);

            Directory.CreateDirectory(Path.Combine(_testRootPath, folder));
            await File.WriteAllTextAsync(fullPath, "Test content");

            // Act
            await _fileService.DeleteFileAsync(filePath);

            // Assert
            Assert.False(File.Exists(fullPath));
        }

        [Fact]
        public async Task DeleteFileAsync_EmptyPath_ThrowsArgumentException()
        {
            // Arrange
            string filePath = "";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _fileService.DeleteFileAsync(filePath));
        }

        public void Dispose()
        {
            try
            {
                Thread.Sleep(100);
                
                if (Directory.Exists(_testRootPath))
                {
                    DirectoryInfo di = new DirectoryInfo(_testRootPath);
                    foreach (var file in di.GetFiles("*", SearchOption.AllDirectories))
                    {
                        file.Attributes = FileAttributes.Normal;
                    }
                    foreach (var dir in di.GetDirectories("*", SearchOption.AllDirectories))
                    {
                        dir.Attributes = FileAttributes.Normal;
                    }
                    
                    Directory.Delete(_testRootPath, true);
                }
            }
            catch (IOException)
            {
                // Log lỗi nếu cần thiết
                // Bỏ qua lỗi khi không thể xóa thư mục
            }
            
            GC.SuppressFinalize(this);
        }
    }
}
