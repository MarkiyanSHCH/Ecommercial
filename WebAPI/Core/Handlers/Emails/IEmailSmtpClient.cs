using System.Threading.Tasks;

using Core.Handlers.Emails.Models;

namespace Core.Handlers.Emails
{
    public interface IEmailSmtpClient
    {
        Task<bool> SendAsync(EmailNotification notification);
    }
}