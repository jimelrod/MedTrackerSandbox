using Eodg.MedicalTracker.Api.Controllers;
using Eodg.MedicalTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Api.Areas.Members
{
    // TODO: Add try/catch, validation shit...
    // TODO: Document...
    public class MembersController : SecuredController
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var member = await _memberService.GetAsync(UserFirebaseId);

            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var member = await _memberService.AddAsync(UserFirebaseId, UserEmail);

            return Ok(member);
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> Deactivate()
        {
            await _memberService.DeactivateAsync(UserFirebaseId);

            return Ok();
        }

        [HttpPut("activate")]
        public async Task<IActionResult> Activate()
        {
            await _memberService.ActivateAsync(UserFirebaseId);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _memberService.DeleteAsync(UserFirebaseId);

            return Ok();
        }
    }
}