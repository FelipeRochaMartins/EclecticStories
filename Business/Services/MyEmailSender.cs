using Business.Services.Base;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class MyEmailSender : IMyEmailSender
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public MyEmailSender(IConfiguration configuration)
    {
        _smtpHost = configuration["EmailSettings:SmtpHost"];
        _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
        _smtpUser = configuration["EmailSettings:SmtpUser"];
        _smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS", EnvironmentVariableTarget.Machine);
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using (var client = new SmtpClient(_smtpHost, _smtpPort))
        {
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
