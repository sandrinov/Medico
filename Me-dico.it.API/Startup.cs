using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using IdentityServer3.AccessTokenValidation;
using Me_dico.it.API.Helpers;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Me_dico.it.API.Startup))]

namespace Me_dico.it.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseIdentityServerBearerTokenAuthentication(new
                                IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Constants.Constants.BaseAddress,
                RequiredScopes = new[] { "medicoApi" }
            });

            var config = new HttpConfiguration();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            WebApiConfig.Register(config);
            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseNinjectWebApi(config);

        }
    }
}
