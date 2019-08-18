using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eodg.MedicalTracker.Api.Authentication
{
    public static class FirebaseAuthenticationExtensions
    {
        #region AuthenticationBuilder Extensions

        public static AuthenticationBuilder AddFirebaseAuthentication(
            this IServiceCollection services,
            string schemeName)
        {
            return AddFirebaseAuthentication(services, schemeName, schemeName, null);
        }

        public static AuthenticationBuilder AddFirebaseAuthentication(
            this IServiceCollection services,
            string schemeName,
            string displayName)
        {
            return AddFirebaseAuthentication(services, schemeName, displayName, null);
        }

        public static AuthenticationBuilder AddFirebaseAuthentication(
            this IServiceCollection services,
            string schemeName,
            Action<FirebaseAuthenticationOptions> options)
        {
            return AddFirebaseAuthentication(services, schemeName, schemeName, options);
        }

        public static AuthenticationBuilder AddFirebaseAuthentication(
            this IServiceCollection services,
            string schemeName,
            string displayName,
            Action<FirebaseAuthenticationOptions> options)
        {
            var authenticationBuilder =
                services
                    .AddAuthentication(schemeName)
                    .AddScheme<FirebaseAuthenticationOptions, FirebaseAuthenticationHandler>(schemeName, displayName, options);

            return authenticationBuilder;
        }

        #endregion

        internal static string ParseAuthorizationBearerToken(this HttpRequest request)
        {
            return
                request
                    .Headers["Authorization"]
                    .ToString()
                    .Replace("Bearer ", string.Empty);
        }
    }
}