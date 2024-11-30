using System;
using System.Collections.Generic;

namespace Web.ViewModels.Booking
{
    public class SelectTicketViewModel
    {
        // Thông tin sự kiện
        public Guid EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventImage { get; set; }

        // Danh sách loại vé
        public List<TicketTypeViewModel> TicketTypes { get; set; }
    }

    public class TicketTypeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }  // VIP, Standard, Early Bird...
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RemainingQuantity { get; set; }
        public int MaxQuantityPerOrder { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndDate { get; set; }
        
        // Các thuộc tính bổ sung
        public bool IsOnSale => DateTime.Now >= SaleStartDate && DateTime.Now <= SaleEndDate;
        public bool IsSoldOut => RemainingQuantity <= 0;
        
        // Các thuộc tính hiển thị
        public string FormattedPrice => Price.ToString("N0") + " VNĐ";
        public string SaleStatus
        {
            get
            {
                if (IsSoldOut) return "Đã bán hết";
                if (!IsOnSale) return "Chưa mở bán";
                return "Đang bán";
            }
        }
    }
}