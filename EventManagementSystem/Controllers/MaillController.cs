using EmailService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    public class MaillController : Controller
    {
        public void Index(string email, EmailSender sender)
        {
            var message = new Message(new string[] { email }, "Test email", "This is a test email", null);
            sender.SendEmail(message);
            // This is a dummy method
        }
    }
}