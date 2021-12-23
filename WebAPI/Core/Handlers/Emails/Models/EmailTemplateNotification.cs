namespace Core.Handlers.Emails.Models
{
    public class EmailTemplateNotification<T> : EmailNotification
    {
        public string EmailTemplateName { get; set; }
        public T Model { get; set; }
    }
}
