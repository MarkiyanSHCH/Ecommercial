using System;
using System.Threading.Tasks;

using Core.Handlers.Emails.Models;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;
using Core.Handlers.Template;

namespace Core.Handlers.Emails
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IEmailSmtpClient _sendEmailClient;
        private readonly IRazorTemplate _templateEngine;
        private readonly ILogger _logger;

        public EmailNotificationService(IEmailSmtpClient sendEmailClient, IRazorTemplate templateEngine, ILogger logger)
        {
            this._sendEmailClient = sendEmailClient;
            this._templateEngine = templateEngine;
            this._logger = logger;
            this._templateEngine.TemplatesPath = "/Views/EmailTemplates/";
        }

        public async Task<bool> NotifyAsync<TModel>(EmailTemplateNotification<TModel> notification)
        {
            try
            {
                notification.Content = await this.RenderEmailBodyAsync<TModel>(
                    emailTemplateName: notification.EmailTemplateName,
                    model: notification.Model);
                return this._sendEmailClient.SendAsync(notification).Result;

            }
            catch (Exception ex)
            {
                this._logger.Error(
                    message: "Failed to send email notification",
                    scope: ApplicationScope.Emails,
                    payload: new { notification },
                    exception: ex);

                return false;
            }
        }

        public async Task<bool> NotifyAsync<TModel>(
           string subject,
           string toAddress,
           string fromAddress,
           string emailTemplateName,
           TModel model)
           => await this.NotifyAsync<TModel>(
               notification: new EmailTemplateNotification<TModel>
               {
                   Subject = subject,
                   ToAddresses = new EmailNotificationAddress { Address = toAddress },
                   FromAddresses = new EmailNotificationAddress { Address = fromAddress },
                   EmailTemplateName = emailTemplateName,
                   Model = model,
               });

        private async Task<string> RenderEmailBodyAsync<TModel>(
            string emailTemplateName,
            TModel model)
        {
            Exception renderException = null;
            string messageBody;
            try
            {
                messageBody = await this._templateEngine.RenderAsync(emailTemplateName, model);
            }
            catch (Exception ex)
            {
                renderException = ex;
                messageBody = null;
            }

            if (string.IsNullOrWhiteSpace(messageBody))
                throw new InvalidOperationException(
                    message: $"Failed to render email body and fallback handler was not passed. Email template={emailTemplateName}",
                    innerException: renderException);

            return messageBody;
        }
    }
}
