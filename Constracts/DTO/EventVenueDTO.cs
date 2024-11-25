using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventVenueDTO : BaseDTO
    {
        public string? VenueName { get; set; }
        public string? City { get; set; } // Tình, thành
        public string? District { get; set; } // Quận, huyện
        public string? Ward { get; set; } // Phường, xã
        public string? Address { get; set; } // Địa chỉ cụ thể
        // Sử dụng để tích hợp bản đồ google vào ứng dụng
        public string? PostalCode { get; set; } // Mã bưu chính
        public decimal? Latitude { get; set; }   // Vĩ độ
        public decimal? Longitude { get; set; }  // Kinh độ
        public string? PlaceId { get; set; }     // Google Places ID
        public string? PhoneNumber { get; set; } // Số điện thoại của địa điểm
        public string? WebsiteUrl { get; set; }  // URL trang web của địa điểm
    }
}
