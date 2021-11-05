using System.Security.Cryptography;

namespace Core.Services.Hashing
{
    internal class HashContext
    {
        public HashAlgorithmName HashAlgorithm { get; set; }
        public int SaltSizeInBytes { get; set; }
        public int HashSizeInBytes { get; set; }
        public int Iterations { get; set; }
        public string Salt { get; set; }
        public string Key { get; set; }
        public bool NeedsUpgrade { get; set; }
    }
}
