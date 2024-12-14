using Web.Areas.Dashboard.Controllers.ManageBookings;

namespace Web.ViewModels
{
    public class PaymentViewModel
    {
        // Thông tin sự kiện
        public int? EventId { get; set; }
        public string? EventTitle { get; set; }
        public string? EventDate { get; set; }
        public string? EventLocation { get; set; }

        // Danh sách loại vé
        public required List<TicketTypeViewModel> TicketTypes { get; set; }
        // Thông tin cá nhân
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsPhoneNumberEditable { get; set; } = false;
        public bool? IsNameEditable { get; set; } = false;
        public decimal TotalAmount { get; set; }
    }
}
