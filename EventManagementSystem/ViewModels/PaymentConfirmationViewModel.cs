namespace Web.ViewModels
{
    public class PaymentConfirmationViewModel
    {
        public int OrderId { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public List<TicketTypeViewModel> TicketTypes { get; set; } = new List<TicketTypeViewModel>();
        public decimal TotalAmount { get; set; }
    }
}
