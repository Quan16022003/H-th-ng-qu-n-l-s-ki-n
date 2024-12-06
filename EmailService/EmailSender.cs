using MimeKit;
using System;
using System.IO;
using System.Linq;

namespace EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;

    public EmailSender(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public async Task SendEmailAsync(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"<h2 style='color:red;'>{message.Content}</h2>",
        };
        
        if (message.Attachments.Any())
        {
            foreach (var attachment in message.Attachments)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
            }
        }
        emailMessage.Body = bodyBuilder.ToMessageBody();
        
        return emailMessage;
    }
    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new MailKit.Net.Smtp.SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
            await client.SendAsync(mailMessage);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}