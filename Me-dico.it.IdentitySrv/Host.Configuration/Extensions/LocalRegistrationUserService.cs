using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using IdentityServerRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServerRepository;
using IdentityServer3.Core.Extensions;

namespace Host.Configuration.Extensions
{
    public class LocalRegistrationUserService : UserServiceBase
    {

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            CustomUser user = null;
            using (IdentityRepository ctx = new IdentityRepository(new IdentityContext()))
            {
                user = ctx.GetUser(context.UserName, context.Password).RetrieveClaims();
            }

            if (user != null)
            {
                context.AuthenticateResult = new AuthenticateResult(user.Subject.ToString(), user.Username);
            }

            return Task.FromResult(0);
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            CustomUser user = null;
            using (IdentityRepository ctx = new IdentityRepository(new IdentityContext()))
            {
                user = ctx.GetUserId(context.Subject.GetSubjectId()).RetrieveClaims();
            }

            // issue the claims for the user
            if (user != null)
            {
                context.IssuedClaims = user.Claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
            }

            return Task.FromResult(0);
        }
    }
}
