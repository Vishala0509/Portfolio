using MailKit.Net.Smtp;  
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using VishalaPortfolio.Models;

public class EmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message, string fromName)
    {
        // Ensure that _emailSettings.From is not null or empty
        if (string.IsNullOrEmpty(_emailSettings.From))
        {
            throw new ArgumentException("Sender email address is not configured properly.");
        }

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(fromName, _emailSettings.From));  // Add from address using configured From email
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        email.Body = new TextPart("plain") { Text = message };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailSettings.From, _emailSettings.AppPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
