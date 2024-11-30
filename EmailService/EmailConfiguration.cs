namespace EmailService;

public class EmailConfiguration
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }

    public override string ToString()
    {
        return $"SmtpServer: {SmtpServer}, SmtpPort: {SmtpPort}, SmtpUsername: {SmtpUsername}, SmtpPassword: {SmtpPassword}, SenderEmail: {SenderEmail}, SenderName: {SenderName}";
    }
}