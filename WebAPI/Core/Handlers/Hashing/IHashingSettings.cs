namespace Core.Handlers.Hashing
{
    public interface IHashingSettings
    {
        string HashAlgorithmName { get; set; }
        int SaltSizeInBytes { get; set; }
        int HashSizeInBytes { get; set; }
        int IterationsCount { get; set; }
    }
}
