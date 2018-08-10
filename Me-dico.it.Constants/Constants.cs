using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Constants
{
    public static class Constants
    {
        // Decommentare per far girare l'applicazione su azure
        public const string BaseAddressMvc = "http://medicomvc.azurewebsites.net/";
        public const string MedicoApiBaseAddress = "http://medicoapi.azurewebsites.net/";

        // Decommentare per far girare l'applicazione in locale
        //public const string BaseAddressMvc = "http://localhost:2627/";
        //public const string MedicoApiBaseAddress = "http://localhost:3943/";
        //================================================================================



        public const string BaseServer = "https://idsrvmedico.azurewebsites.net/";
        public const string BaseAddress = "https://idsrvmedico.azurewebsites.net/core";
        public const string AuthorityEndpoint = BaseServer + "/identity";
        public const string AuthorizeEndpoint = BaseAddress + "/connect/authorize";
        public const string LogoutEndpoint = BaseAddress + "/connect/endsession";
        public const string TokenEndpoint = BaseAddress + "/connect/token";
        public const string UserInfoEndpoint = BaseAddress + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = BaseAddress + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = BaseAddress + "/connect/revocation";
        public static readonly object ClaimTypes;


        //public const string BaseAddress = "https://localhost:44305/identity";
        //public const string TokenEndpoint = BaseAddress + "/connect/token";
        //public const string AuthorizeEndpoint = BaseAddress + "/connect/authorize";
        //public const string UserInfoEndpoint = BaseAddress + "/connect/userinfo";

    }
}
