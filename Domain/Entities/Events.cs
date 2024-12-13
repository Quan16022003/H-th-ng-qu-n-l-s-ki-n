using Domain.CustomAttribute;
using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Events : BaseEntity
    {
        public string Slug { get; set; }
        public string OrganizerId { get; set; }
        public ApplicationUser? Organizer { get; set; }

        #region Detail
        public int CategoryId { get; set; }
        public CategoryEvents? CategoryEvent { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        #endregion

        #region Timing
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        #endregion

        #region Venue
        public string? VenueName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; } // Tình, thành
        public string? District { get; set; } // Quận, huyện
        public string? Ward { get; set; } // Phường, xã
        public string? Address { get; set; } // Địa chỉ cụ thể
        // Sử dụng để tích hợp bản đồ google vào ứng dụng
        public string? PostalCode { get; set; } // Mã bưu chính
        public decimal? Latitude { get; set; }   // Vĩ độ
        public decimal? Longitude { get; set; }  // Kinh độ

        [AllowEmptyOrNull]
        public string? PlaceId { get; set; }     // Google Places ID

        [AllowEmptyOrNull]
        public string? PhoneNumber { get; set; } // Số điện thoại của địa điểm

        [AllowEmptyOrNull]
        public string? WebsiteUrl { get; set; }  // URL trang web của địa điểm
        #endregion

        #region Media
        public string? ThumbnailUrl { get; set; }

        [AllowEmptyOrNull]
        public string? CoverUrl { get; set; }
        #endregion

        [AllowEmptyOrNull]
        public IEnumerable<Tickets>? Tickets { get; set; }

        [AllowEmptyOrNull]
        public IEnumerable<Orders>? Orders { get; set; }

        [AllowEmptyOrNull]
        public IEnumerable<Attendees>? Attendees { get; set; }
    }
}
