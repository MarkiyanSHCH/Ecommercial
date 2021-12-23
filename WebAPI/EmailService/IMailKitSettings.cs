namespace WebAPI.API.MailKit
{
    public interface IMailKitSettings
    {
        string EmailFrom { get; set; }
        public string NameFrom { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        int Port { get; set; }
    }
}