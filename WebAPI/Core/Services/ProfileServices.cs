using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class ProfileServices
    {
        private readonly IProfileRepository _profileRepository;
        private readonly HashingService _hashingService;

        public ProfileServices(IProfileRepository profileRepository, HashingService hashingService)
            => (this._profileRepository, this._hashingService) = (profileRepository, hashingService);

        public Account GetProfileById(int userId)
            => this._profileRepository.GetProfileById(userId);

        public bool UpdateName(int userId, string name)
            => this._profileRepository.UpdateName(userId, name);

        public bool UpdatePassword(int userId, string password)
        {
            string hashResult = this._hashingService.Hash(password);

            return hashResult == null && this._profileRepository.UpdatePassword(userId, hashResult);
        }
    }
}