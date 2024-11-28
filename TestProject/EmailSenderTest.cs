using Xunit;
using Moq;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;

namespace EmailService.Tests
{
    public class EmailSenderTest
    {
        private readonly EmailConfiguration _emailConfig;
        
        public EmailSenderTest(EmailConfiguration emailConfiguration)
        {
            _emailConfig = emailConfiguration;
        }

        [Fact]
        public void SendEmail_ValidMessage_EmailSentSuccessfully()
        {
            // Arrange
            var formFiles = new FormFileCollection();
            var message = new Message(
                new[] { "nhq16022003@gmail.com" },
                "Test Subject",
                "Test Content",
                formFiles
            );
            
            var emailSender = new EmailSender(_emailConfig);

            // Act & Assert
            var exception = Record.Exception(() => emailSender.SendEmail(message));
            Assert.Null(exception);
        }

        [Fact]
        public void SendEmail_NullMessage_ThrowsArgumentNullException()
        {
            // Arrange
            var emailSender = new EmailSender(_emailConfig);

            // Act & Assert  
            Assert.Throws<ArgumentNullException>(() => emailSender.SendEmail(null));
        }

        [Fact]
        public void SendEmail_EmptyRecipients_ThrowsArgumentException()
        {
            // Arrange
            var formFiles = new FormFileCollection();
            var message = new Message(
                new List<string>(),
                "Test Subject", 
                "Test Content",
                formFiles
            );

            var emailSender = new EmailSender(_emailConfig);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => emailSender.SendEmail(message));
        }
    }
}
