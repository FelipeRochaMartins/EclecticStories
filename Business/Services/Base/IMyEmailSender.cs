

namespace Business.Services.Base
{
    public interface IMyEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
