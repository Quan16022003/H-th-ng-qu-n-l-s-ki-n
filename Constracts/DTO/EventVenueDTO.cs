using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventVenueDTO : BaseDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn địa điểm bằng cách tìm kiếm")]
        [Display(Name = "Tên địa điểm")]
        public string? VenueName { get; set; }
        
        [Display(Name = "Tên đường")]
        public string? Street { get; set; }
        
        [Display(Name = "Tên thành phố")]
        public string? City { get; set; } // Tình, thành
        
        [Display(Name = "Quận")]
        public string? District { get; set; } // Quận, huyện
        
        [Display(Name = "Phường")]
        public string? Ward { get; set; } // Phường, xã
        public string? Address { get; set; } // Địa chỉ cụ thể

        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; } // Mã bưu chính
        
        [Display(Name = "Vĩ độ")]
        public decimal? Latitude { get; set; }   // Vĩ độ

        [Display(Name = "Kinh độ")]
        public decimal? Longitude { get; set; }  // Kinh độ
        public string? PlaceId { get; set; }     // Google Places ID
        public string? PhoneNumber { get; set; } // Số điện thoại của địa điểm
        public string? WebsiteUrl { get; set; }  // URL trang web của địa điểm
    }
}
