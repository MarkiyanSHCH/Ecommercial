using System.Security.Cryptography;

namespace Core.Services.Hashing
{
    internal class ParametersPolicy
    {
        public HashAlgorithmName HashAlgorithm { get; set; }
        public int MinInputCharactersSize { get; set; }
        public int MinSaltSizeInBytes { get; set; }
        public int MinHashSizeInBytes { get; set; }
        public int MinIterationsCount { get; set; }
    }
}
