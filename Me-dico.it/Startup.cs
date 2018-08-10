
using Me_dico.it.WebClient.Helpers;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json.Linq;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Thinktecture.IdentityModel.Client;

[assembly: OwinStartupAttribute(typeof(Me_dico.it.Startup))]
namespace Me_dico.it
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "unique_user_key";

            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "medicoClientMvc",
                UseTokenLifetime = false,
                Authority = Constants.Constants.BaseAddress,
                RedirectUri = Constants.Constants.BaseAddressMvc,
                PostLogoutRedirectUri = Constants.Constants.BaseAddressMvc,
                ResponseType = "code id_token token",
                Scope = "openid email profile roles medicoApi offline_access",

                SignInAsAuthenticationType = "Cookies",

                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    RedirectToIdentityProvider = n =>
                    {
                        // if signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }

                        }

                        return Task.FromResult(0);
                    },

                    MessageReceived = async n =>
                    {
                        EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);
                        EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.AccessToken);
                        var userInfo = await EndpointAndTokenHelper.CallUserInfoEndpoint(n.ProtocolMessage.AccessToken);    
                    },

                    SecurityTokenValidated = async n =>
                    {
                        var userInfo = await EndpointAndTokenHelper.CallUserInfoEndpoint(n.ProtocolMessage.AccessToken);


                        // use the authorization code to get a refresh token 
                        var tokenEndpointClient = new OAuth2Client(
                            new Uri(Constants.Constants.TokenEndpoint),
                            "medicoClientMvc", "secret");

                        var tokenResponse = await tokenEndpointClient.RequestAuthorizationCodeAsync(
                            n.ProtocolMessage.Code, Constants.Constants.BaseAddressMvc);



                        var givenNameClaim = new Claim(
                            Thinktecture.IdentityModel.Client.JwtClaimTypes.GivenName,
                            userInfo.Value<string>("given_name"));

                        var familyNameClaim = new Claim(
                            Thinktecture.IdentityModel.Client.JwtClaimTypes.FamilyName,
                            userInfo.Value<string>("family_name"));

                        var roles = userInfo.Value<JArray>("role").ToList();

                        var newIdentity = new ClaimsIdentity(
                           n.AuthenticationTicket.Identity.AuthenticationType,
                           Thinktecture.IdentityModel.Client.JwtClaimTypes.GivenName,
                           Thinktecture.IdentityModel.Client.JwtClaimTypes.Role);

                        newIdentity.AddClaim(givenNameClaim);
                        newIdentity.AddClaim(familyNameClaim);

                        foreach (var role in roles)
                        {
                            newIdentity.AddClaim(new Claim(
                            Thinktecture.IdentityModel.Client.JwtClaimTypes.Role,
                            role.ToString()));
                        }

                        var issuerClaim = n.AuthenticationTicket.Identity
                            .FindFirst(Thinktecture.IdentityModel.Client.JwtClaimTypes.Issuer);
                        var subjectClaim = n.AuthenticationTicket.Identity
                            .FindFirst(Thinktecture.IdentityModel.Client.JwtClaimTypes.Subject);

                        newIdentity.AddClaim(new Claim("unique_user_key",
                            issuerClaim.Value + "_" + subjectClaim.Value));


                        newIdentity.AddClaim(new Claim("refresh_token", tokenResponse.RefreshToken));
                        //newIdentity.AddClaim(new Claim("access_token", tokenResponse.AccessToken));
                        newIdentity.AddClaim(new Claim("expires_at",
                            DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToLocalTime().ToString()));

                        newIdentity.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));
                        newIdentity.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        //// use the authorization code to get a refresh token 
                        //var oauth2Client = new OAuth2Client(
                        //    new Uri(Constants.Constants.TokenEndpoint),
                        //    "medicoClientMvc", "secret");



                        n.AuthenticationTicket = new AuthenticationTicket(
                            newIdentity,
                            n.AuthenticationTicket.Properties);

                    },

                }
            });
        }
    }
}
