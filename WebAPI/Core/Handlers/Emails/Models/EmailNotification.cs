namespace Core.Handlers.Emails.Models
{
    public class EmailNotification
    {
        public string Subject { get; set; }
        public EmailNotificationAddress ToAddresses { get; set; }
        public EmailNotificationAddress FromAddresses { get; set; }
        public string Content { get; set; }
    }
}