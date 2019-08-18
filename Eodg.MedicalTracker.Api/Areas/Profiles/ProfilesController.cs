using Eodg.MedicalTracker.Api.Controllers;
using Eodg.MedicalTracker.Api.Filters;
using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Api.Areas.Profiles
{
    // TODO: Try/Catch...?
    public partial class ProfilesController : SecuredController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;

        public ProfilesController(
            IAuthorizationService authorizationService,
            IProfileService profileService)
        {
            _authorizationService = authorizationService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool? isActive)
        {
            var profiles = await _profileService.GetAsync(UserFirebaseId, isActive);

            return Ok(profiles);
        }

        [HttpGet("{id}")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _profileService.GetAsync(id);

            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProfileRequestBody requestBody)
        {
            var profile = await _profileService.AddAsync(UserFirebaseId, requestBody.DisplayName, requestBody.Notes);

            return Ok(profile);
        }

        [HttpPut("{id}")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> Put(int id, ProfileRequestBody requestBody)
        {
            var profile = await _profileService.UpdateAsync(UserFirebaseId, id, requestBody.DisplayName, requestBody.Notes);

            return Ok(profile);
        }

        [HttpPut("{id}/activate")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> Activate(int id)
        {
            var profile = await _profileService.ActivateAsync(UserFirebaseId, id);

            return Ok(profile);
        }

        [HttpPut("{id}/deactivate")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> Deactivate(int id)
        {
            var profile = await _profileService.DeactivateAsync(UserFirebaseId, id);

            return Ok(profile);
        }

        [HttpDelete("{id}")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> Delete(int id)
        {
            await _profileService.DeleteAsync(id);

            return Ok();
        }

        [HttpPost("{id}/owners")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> AddOwner(int id, ProfileOwnerRequestBody requestBody)
        {
            var profile = await _profileService.AddOwnerAsync(id, requestBody.FirebaseId);

            return Ok(profile);
        }

        [HttpDelete("{id}/owners")]
        [OwnableResourceFilter(typeof(IProfileService))]
        public async Task<IActionResult> DeleteOwnerOwner(int id, ProfileOwnerRequestBody requestBody)
        {
            var profile = await _profileService.RemoveOwnerAsync(id, requestBody.FirebaseId);

            return Ok(profile);
        }
    }
}