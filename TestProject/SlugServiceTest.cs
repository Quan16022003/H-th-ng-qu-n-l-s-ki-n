using Services;
using Services.Abtractions;
using Xunit;

namespace TestProject;
public class SlugServiceTests
{
    private readonly ISlugService _slugService;

    public SlugServiceTests()
    {
        _slugService = new SlugService();
    }

    [Theory]
    [InlineData("Cách tạo Slug trong ASP.NET", "cach-tao-slug-trong-asp-net")]
    [InlineData("   Nhiều  Khoảng Trắng   ", "nhieu-khoang-trang")]
    [InlineData("Tiêu đề có ký tự đặc biệt!@#$%", "tieu-de-co-ky-tu-dac-biet")]
    public void GenerateSlug_ShouldReturnValidSlug(string input, string expected)
    {
        string result = _slugService.GenerateSlug(input);

        Assert.Equal(expected, result);
    }
}
