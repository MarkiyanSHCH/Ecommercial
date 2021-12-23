using Data;

namespace WebAPI.Models.Settings
{
    public class DbSettings: IDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
