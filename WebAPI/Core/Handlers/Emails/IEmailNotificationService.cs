using System.Threading.Tasks;

using Core.Handlers.Emails.Models;

namespace Core.Handlers.Emails
{
    public interface IEmailNotificationService
    {
        Task<bool> NotifyAsync<TModel>(EmailTemplateNotification<TModel> notification);
        Task<bool> NotifyAsync<TModel>(
           string subject,
           string toAddress,
           string fromAddress,
           string emailTemplateName,
           TModel model);
    }
}