using Eodg.MedicalTracker.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Interfaces
{
    public interface IProfileMedicationService
    {
        ProfileMedication Get(int id);
        Task<ProfileMedication> GetAsync(int id);
        IEnumerable<ProfileMedication> GetByProfileId(int profileId);
        Task<IEnumerable<ProfileMedication>> GetByProfileIdAsync(int profileId);

        ProfileMedication Add(string firebaseId, ProfileMedication profileMedication);
        Task<ProfileMedication> AddAsync(string firebaseId, ProfileMedication profileMedication);

        ProfileMedication Update(string firebaseId, ProfileMedication profileMedication);
        Task<ProfileMedication> UpdateAsync(string firebaseId, ProfileMedication profileMedication);

        ProfileMedication Activate(string firebaseId, int id);
        Task<ProfileMedication> ActivateAsync(string firebaseId, int id);

        ProfileMedication Deactivate(string firebaseId, int id);
        Task<ProfileMedication> DeactivateAsync(string firebaseId, int id);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}