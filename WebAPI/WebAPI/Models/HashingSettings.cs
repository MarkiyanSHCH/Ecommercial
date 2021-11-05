using Core.Services.Hashing;

namespace WebAPI.Models
{
    public class HashingSettings: IHashingSettings
    {
        public string HashAlgorithmName { get; set; }
        public int SaltSizeInBytes { get; set; }
        public int HashSizeInBytes { get; set; }
        public int IterationsCount { get; set; }
    }
}
