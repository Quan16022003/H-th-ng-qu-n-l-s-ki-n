using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class SelectTicketViewModel
    {
        // Thông tin sự kiện
        public int EventId { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDate { get; set; }
        public string? EventLocation { get; set; }
        public string? EventImage { get; set; }

        // Danh sách loại vé
        public List<TicketTypeViewModel> TicketTypes { get; set; }
    }

    public class TicketTypeViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int QuantityBuy { get; set; }
        public int? QuantityAvailable { get; set; }
        public int? QuantitySold { get; set; }
        public int? RemainingQuantity => QuantityAvailable - QuantitySold;
        public int? MaxPerPerson { get; set; }

        // Các thuộc tính bổ sung
        public bool IsSoldOut => RemainingQuantity <= 0;

        // Các thuộc tính hiển thị
        public string FormattedPrice => Price?.ToString("N0") + " VNĐ";
        public string SaleStatus => IsSoldOut ? "Đã bán hết" : "Đang bán";
    }
}