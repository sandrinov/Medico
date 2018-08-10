using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Security.Claims;
namespace IdentityServer3.Host.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "MVC Client",
                    ClientId = "medicoClientMvc",
                    Flow = Flows.Hybrid,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = new List<string>
                    {
                        "http://medicomvc.azurewebsites.net/",
                        "http://localhost:2627/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://medicomvc.azurewebsites.net/",
                        "http://localhost:2627/"
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess,
                        "medicoApi"
                    },
                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Jwt,
                    LogoutSessionRequired = true
                },
                new Client
                {
                    ClientName = "Native Client",
                    ClientId = "nativeMedico",
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    //RedirectUris = new List<string>
                    //{
                    //       
                    //},
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Roles,
                        "medicoApi"
                    }
                }
            };
        }
    }
}