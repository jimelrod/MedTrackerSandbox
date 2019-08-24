using Eodg.MedicalTracker.Api.Exceptions;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Api.Authentication
{
    public class FirebaseAuthenticationHandler : AuthenticationHandler<FirebaseAuthenticationOptions>
    {
        public FirebaseAuthenticationHandler(
            IOptionsMonitor<FirebaseAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
                : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string jwt;
            FirebaseToken token;

            try
            {
                jwt = Request.ParseAuthorizationBearerToken();
                token = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(jwt);

                if (!(bool)token.Claims["email_verified"])
                {
                    throw new UnverifiedAccountException();
                }
            }
            catch (UnverifiedAccountException ex)
            {
                // TODO: Make it so that the client knows it is an email verification error...
                Context.Response.StatusCode = 401;
                return AuthenticateResult.Fail(ex);
            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = 401;
                return AuthenticateResult.Fail(ex);
            }

            var ticket = BuildTicket(token);

            return AuthenticateResult.Success(ticket);
        }

        private AuthenticationTicket BuildTicket(FirebaseToken token)
        {
            var claims = BuildClaims(token);
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationTicket(principal, Scheme.Name);
        }

        private static Claim[] BuildClaims(FirebaseToken token)
        {
            return new[]
            {
                new Claim(ClaimTypes.Name, token.Claims["name"].ToString()),
                new Claim(ClaimTypes.Email, token.Claims["email"].ToString()),
                new Claim(ClaimTypes.NameIdentifier, token.Claims["user_id"].ToString())
            };
        }
    }
}