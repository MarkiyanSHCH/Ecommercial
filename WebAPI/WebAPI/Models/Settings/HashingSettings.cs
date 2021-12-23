using Core.Handlers.Hashing;

namespace WebAPI.Models.Settings
{
    public class HashingSettings: IHashingSettings
    {
        public string HashAlgorithmName { get; set; }
        public int SaltSizeInBytes { get; set; }
        public int HashSizeInBytes { get; set; }
        public int IterationsCount { get; set; }
    }
}