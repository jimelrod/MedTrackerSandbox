using Eodg.MedicalTracker.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Interfaces
{
    public interface IProfileService
    {
        Profile Get(int id);
        Task<Profile> GetAsync(int id);

        IEnumerable<Profile> Get(string firebaseId, bool? isActive = true);
        Task<IEnumerable<Profile>> GetAsync(string firebaseId, bool? isActive = true);

        Profile Add(string firebaseId, string displayName, string notes);
        Task<Profile> AddAsync(string firebaseId, string displayName, string notes);

        Profile Update(string firebaseId, int profileId, string displayName, string notes);
        Task<Profile> UpdateAsync(string firebaseId, int profileId, string displayName, string notes);

        Profile Activate(string firebaseId, int id);
        Task<Profile> ActivateAsync(string firebaseId, int id);

        Profile Deactivate(string firebaseId, int id);
        Task<Profile> DeactivateAsync(string firebaseId, int id);

        void Delete(int id);
        Task DeleteAsync(int id);

        Profile AddOwner(int profileId, string firebaseId);
        Task<Profile> AddOwnerAsync(int profileId, string firebaseId);

        Profile AddOwner(int profileId, int memberId);
        Task<Profile> AddOwnerAsync(int profileId, int memberId);

        Profile RemoveOwner(int profileId, string firebaseId);
        Task<Profile> RemoveOwnerAsync(int profileId, string firebaseId);

        Profile RemoveOwner(int profileId, int memberId);
        Task<Profile> RemoveOwnerAsync(int profileId, int memberId);
    }
}