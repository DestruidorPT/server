using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Bit.Core.Models.Table;
using Bit.Core.Enums;
using Bit.Core.Repositories;
using U2F.Core.Models;
using System;
using Bit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Fido2NetLib;

namespace Bit.Core.Identity
{
    /// <summary>
    /// Class that handles the authentication token of the two-factor.
    /// </summary>
    public class Fido2TokenProvider : IUserTwoFactorTokenProvider<User>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GlobalSettings _globalSettings;

        public Fido2TokenProvider(
            IServiceProvider serviceProvider,
            GlobalSettings globalSettings)
        {
            _serviceProvider = serviceProvider;
            _globalSettings = globalSettings;
        }

        /// <summary>
        /// Will check if the user and the system can create the authentication token.
        /// </summary>
        public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            // Check if the user has the Premium service.
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            if (!(await userService.CanAccessPremium(user)))
            {
                return false;
            }

            // Check if the user has the FIDO2 two-factor enable
            var provider = user.GetTwoFactorProvider(TwoFactorProviderType.Fido2);
            if (provider == null)
            {
                return false;
            }

            return await userService.TwoFactorProviderIsEnabledAsync(TwoFactorProviderType.Fido2, user);
        }

        /// <summary>
        /// Will generate an authentication token, which contains the FIDO2 information in JSON.
        /// </summary>
        public async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
        {
            // Check if the user has the Premium service.
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            if (!(await userService.CanAccessPremium(user)))
            {
                return null;
            }

            // Check if the user has the FIDO2 two-factor enable.
            var provider = user.GetTwoFactorProvider(TwoFactorProviderType.Fido2);
            if (provider == null)
            {
                return null;
            }

            // This will go get the generated FIDO2 information for authentication.
            // The second parameter of the command "StartFido2AuthenticationAsync" should be the origin of the request, 
            // but i wasn't able to get it. And I decided that the origin default will be url vault, this will cause android 
            // not work, so instead of android using this information it will request a new one from API.
            // This is temporarily fixed until we can get the origin parameter from the user.
            var response = await userService.StartFido2AuthenticationAsync(user, _globalSettings.BaseServiceUri.Vault);
            return $"{response.ToJson()}";
        }

        /// <summary>
        /// Here it will be check if the token sent by the client is correctly signed.
        /// </summary>
        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
        {
            // Check if User is allowed to use Premium service and if the token sent is valid.
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            if (!(await userService.CanAccessPremium(user)) || string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            // Receive the information sent by the user, check if was a successful transformation of the data.
            var tokenObject = BaseModel.FromJson<AuthenticatorAssertionRawResponse>(token);
            if(tokenObject == null)
            {
                return false;
            }

            // This will check if the responce from the client is valid.
            return await userService.CompleteFido2AuthenticationAsync(user, tokenObject);
        }
    }
}
