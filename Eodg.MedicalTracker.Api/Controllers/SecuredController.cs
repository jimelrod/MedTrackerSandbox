using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Eodg.MedicalTracker.Api.Controllers
{
    [Authorize]
    public abstract class SecuredController : BaseController
    {
        private string _userFirebaseId;
        private string _userEmail;
        private string _userDisplayName;

        protected string UserFirebaseId
        {
            get
            {
                if (string.IsNullOrEmpty(_userFirebaseId))
                {
                    _userFirebaseId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }

                return _userFirebaseId;
            }
        }
        protected string UserEmail
        {
            get
            {
                if (string.IsNullOrEmpty(_userEmail))
                {
                    _userEmail = User.FindFirst(ClaimTypes.Email).Value;
                }

                return _userEmail;
            }
        }

        protected string UserDisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_userDisplayName))
                {
                    _userDisplayName = User.FindFirst(ClaimTypes.Email).Value;
                }

                return _userDisplayName;
            }
        }
    }
}