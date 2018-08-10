using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Me_dico.it.API.Helpers
{
         public class AuthorizationManager : ResourceAuthorizationManager
        {
            public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
            {
                switch (context.Resource.First().Value)
                {
                    case "questions":
                        return AuthorizeQuestion(context);
                    default:
                        return Nok();
                }

            }


            private Task<bool> AuthorizeQuestion(ResourceAuthorizationContext context)
            {
                switch (context.Action.First().Value)
                {
                    case "Read":
                    // to be able to read an question , the user must be in the
                    // WebReadUser role
                    //var elem = context.Principal.Claims.Select(z => z.Value == "WebReadUser");

                        return Eval(context.Principal.HasClaim("role", "WebReadUser") ||
                                    context.Principal.HasClaim(@"http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "WebReadUser"));
                    case "Write":
                        // to be able to create an question, the user must be in the
                        // WebWriteUser role
                        return Eval(context.Principal.HasClaim("role", "WebWriteUser") ||
                                    context.Principal.HasClaim(@"http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "WebWriteUser"));
                    default:
                        return Nok();
                }
            }
        }
}
