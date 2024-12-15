using Constracts.DTO;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;

namespace Web.Areas.Dashboard.Controllers
{
    [Authorize(Policy = "QRCode")]
    [Area("Dashboard")]
    public class QRCodeController : Controller
    {
        private readonly IAttendeeService attendeeService;

        public QRCodeController(IServiceManager serviceManager)
        {
            attendeeService = serviceManager.AttendeeService;
        }

        [HttpGet]
        public IActionResult Scan()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ProcessQRCode([FromBody] QRCodeData qrCodeData)
        {


            if (qrCodeData == null || string.IsNullOrEmpty(qrCodeData.qrCode))
            {
                return BadRequest("Mã QR không hợp lệ.");
            }

            if (int.TryParse(qrCodeData.qrCode, out int numericValue))
            {
                var _attendee = await attendeeService.GetAttendeeByIdAsync(numericValue);
                if (_attendee == null)
                {
                    return NotFound(new { Message = "Attendee not found." });
                }

                var _attendeeCheckIn = _attendee.Adapt<CheckinAttendeeDTO>();

                try
                {
                    _attendeeCheckIn.HasArrived = true;
                    _attendeeCheckIn.ArrivalTime = DateTime.Now;
                    await attendeeService.UpdateCheckinAttendeeAsync(_attendeeCheckIn);
                    return Ok(new { Message = "Check-in successful." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
                }
            }
            else
            {
                return BadRequest("Mã QR không hợp lệ. Vui lòng nhập mã số.");
            }
        }

        public class QRCodeData
        {
            public string qrCode { get; set; }
        }
    }
}
