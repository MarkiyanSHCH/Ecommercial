using WebAPI.API.MailKit;

namespace WebAPI.Models.Settings
{
    public class MailKitSettings : IMailKitSettings
    {
        public string EmailFrom { get; set; }
        public string NameFrom { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
    }
}