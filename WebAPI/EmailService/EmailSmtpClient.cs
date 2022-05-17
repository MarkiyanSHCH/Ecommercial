using System;
using System.Threading.Tasks;

using Core.Handlers.Emails;
using Core.Handlers.Emails.Models;
using Core.Handlers.Logging;
using Core.Handlers.Logging.Models;

using MailKit.Net.Smtp;

using MimeKit;

namespace WebAPI.API.MailKit
{
    public class EmailStmpClient : IEmailSmtpClient
    {
        private readonly IMailKitSettings _settings;
        private readonly ILogger _logger;

        public EmailStmpClient(IMailKitSettings settings, ILogger logger)
         => (this._settings, this._logger) = (settings, logger);

        public async Task<bool> SendAsync(EmailNotification notification)
        {
            try
            {
                var builder = new BodyBuilder
                {
                    HtmlBody = notification.Content
                };

                var message = new MimeMessage
                {
                    Subject = notification.Subject,
                    Body = builder.ToMessageBody()
                };

                message.To.Add(new MailboxAddress(notification.ToAddresses.Name, notification.ToAddresses.Address));
                message.From.Add(new MailboxAddress(notification.FromAddresses.Name, notification.FromAddresses.Address));

                using var emailClient = new SmtpClient();

                await emailClient.ConnectAsync(
                    host: this._settings.Server,
                    port: this._settings.Port);

                await emailClient.AuthenticateAsync(this._settings.UserName, this._settings.Password);
                await emailClient.SendAsync(message);
                await emailClient.DisconnectAsync(quit: true);

                return true;
            }
            catch (Exception ex)
            {
                this._logger.Error(
                    message: "Failed to send email through provided SMTP client.",
                    scope: ApplicationScope.Emails,
                    payload: new { notification },
                    exception: ex);

                return false;
            }
        }
    }
}