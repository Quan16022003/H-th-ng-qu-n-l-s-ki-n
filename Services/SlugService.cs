using Services.Abtractions;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace Services
{
    public partial class SlugService : ISlugService
    {
        private readonly string[] _vietnameseSigns =
            [
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            ];

        [GeneratedRegex(@"[^a-zA-Z0-9\s-]")]
        private static partial Regex SpecialCharsRegex();

        [GeneratedRegex(@"\s+")]
        private static partial Regex WhitespaceRegex();

        [GeneratedRegex(@"-+")]
        private static partial Regex MultipleDashRegex();

        public string GenerateSlug(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            
            for (int i = 1; i < _vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < _vietnameseSigns[i].Length; j++)
                {
                    input = input.Replace(_vietnameseSigns[i][j], _vietnameseSigns[0][i - 1]);
                }
            }

            input = input.Replace(".", "-");

            // Loại bỏ ký tự đặc biệt và thay thế bằng dấu gạch ngang
            input = SpecialCharsRegex().Replace(input.Trim(), "");
            input = WhitespaceRegex().Replace(input, "-");
            input = MultipleDashRegex().Replace(input, "-");

            // Xóa dấu gạch ngang ở đầu và cuối
            input = input.Trim('-');

            return input.ToLower();
        }
    }
}