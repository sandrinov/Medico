using IdentityServer3.Core;
using IdentityServerRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Host.Configuration.Extensions
{
    public static class ClaimExtensionMethodQuerable
    { 
        public static CustomUser RetrieveClaims(this CustomUser u)
        {
            if (u == null) return null;

            u.Claims = new List<Claim>();
            u.Claims.Add(new Claim(Constants.ClaimTypes.GivenName, u.Username));
            u.Claims.Add(new Claim(Constants.ClaimTypes.FamilyName, u.Username));
            switch (u.Role)
            {
                case Me_dico.it.Constants.EnumUserRole.SuperAdmin:
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "SuperAdmin"));
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "WebReadUser"));
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "WebWriteUser"));
                    break;
                case Me_dico.it.Constants.EnumUserRole.Admin:
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "Admin"));
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "WebReadUser"));
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "WebWriteUser"));
                    break;
                case Me_dico.it.Constants.EnumUserRole.User:
                    u.Claims.Add(new Claim(Constants.ClaimTypes.Role, "User"));
                    break;
                default:
                    break;
            }
            return u;
        }
    }
}
