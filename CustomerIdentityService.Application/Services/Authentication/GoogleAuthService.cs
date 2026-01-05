using CustomerIdentityService.Core.Dtos.Google;
using CustomerIdentityService.Core.Interfaces.Security;
using CustomerIdentityService.Core.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Application.Services.Authentication
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly GoogleWebApiSettings _googleWebApiSettings;
        public GoogleAuthService(IOptions<GoogleWebApiSettings> googleWebApiSettings) {
            _googleWebApiSettings = googleWebApiSettings.Value;
        }
        public async Task<GoogleUserInfoDto?> VerifyGoogleTokenAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleWebApiSettings.client_id }
                });
                return new GoogleUserInfoDto(payload.Email, payload.Name, payload.Picture, payload.Subject);
            }
            catch
            {
                return null;
            }
        }

    }
}
