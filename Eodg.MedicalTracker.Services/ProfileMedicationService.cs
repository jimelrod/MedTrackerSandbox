using AutoMapper;
using Eodg.MedicalTracker.Dto;
using Eodg.MedicalTracker.Services.Data.Interfaces;
using Eodg.MedicalTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services
{
    public class ProfileMedicationService : IProfileMedicationService
    {
        private readonly IDataService<Domain.ProfileMedication> _profileMedicationDataService;

        private readonly IMapper _mapper;

        public ProfileMedicationService(
            IDataService<Domain.ProfileMedication> profileMedicationDataService,
            IMapper mapper)
        {
            _profileMedicationDataService = profileMedicationDataService;

            _mapper = mapper;
        }

        public ProfileMedication Activate(string firebaseId, int id)
        {
            var profileMedication = _profileMedicationDataService.SetActivation(firebaseId, id, true);

            return _mapper.Map<ProfileMedication>(profileMedication);
        }

        public async Task<ProfileMedication> ActivateAsync(string firebaseId, int id)
        {
            var profileMedication = await _profileMedicationDataService.SetActivationAsync(firebaseId, id, true);

            return _mapper.Map<ProfileMedication>(profileMedication);
        }

        public ProfileMedication Add(string firebaseId, ProfileMedication profileMedication)
        {
            // var x = _profileMedicationDataService.Add(profileMedication);

            throw new System.NotImplementedException();
        }

        public async Task<ProfileMedication> AddAsync(string firebaseId, ProfileMedication profileMedication)
        {
            throw new System.NotImplementedException();
        }

        public ProfileMedication Deactivate(string firebaseId, int id)
        {
            var profileMedication = _profileMedicationDataService.SetActivation(firebaseId, id, true);

            return _mapper.Map<ProfileMedication>(profileMedication);
        }

        public async Task<ProfileMedication> DeactivateAsync(string firebaseId, int id)
        {
            var profileMedication = await _profileMedicationDataService.SetActivationAsync(firebaseId, id, true);

            return _mapper.Map<ProfileMedication>(profileMedication);
        }

        public void Delete(int id)
        {
            var profileMedication = _profileMedicationDataService.Get(id);
            
            _profileMedicationDataService.Delete(profileMedication);
        }

        public async Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public ProfileMedication Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ProfileMedication> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProfileMedication> GetByProfileId(int profileId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ProfileMedication>> GetByProfileIdAsync(int profileId)
        {
            throw new System.NotImplementedException();
        }

        public ProfileMedication Update(string firebaseId, ProfileMedication profileMedication)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ProfileMedication> UpdateAsync(string firebaseId, ProfileMedication profileMedication)
        {
            throw new System.NotImplementedException();
        }
    }
}