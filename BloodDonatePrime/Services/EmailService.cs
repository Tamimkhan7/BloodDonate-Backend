using BloodBankAPI.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly SmtpSettings _smtp;

    public EmailService(IOptions<SmtpSettings> smtp)
    {
        _smtp = smtp.Value;
    }

    public void Send(string to, string subject, string body)
    {
        var client = new SmtpClient(_smtp.Host, _smtp.Port)
        {
            Credentials = new NetworkCredential(
                _smtp.Username,
                _smtp.Password),
            EnableSsl = _smtp.EnableSsl
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_smtp.Username),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mail.To.Add(to);
        client.Send(mail);
    }
}
