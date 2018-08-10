using MedicoRegister.portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedicoRegister.portable.Services
{
    public sealed class MedicoServiceClient
    {
        private String _urlServer;
        private static MedicoServiceClient _restClient;

        private MedicoServiceClient()
        {
            _urlServer = "http://medicoapi.azurewebsites.net/api/";

        }

        public static MedicoServiceClient GetClient()
        {
            if (_restClient == null)
                _restClient = new MedicoServiceClient();

            return _restClient;
        }

        async public Task<String> CanRegister(RegistrationData data)
        {
            var url = _urlServer + "CanRegister";
            var responseString = "";
            // Create an HTTP web request using the URL:
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "PhoneNumber", data.PhoneNumber },
                    { "MedicoCode", data.MedicoCode },
                    { "ActivationCode", data.ActivationCode },
                    { "RegistrationTime", data.RegistrationTime }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(url, content);
                responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            #region oldcode
            //request.ContentType = "application/json";
            //request.Method = "POST";

            //using (WebResponse response = await request.GetResponseAsync())
            //{
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        StreamReader readStream = new StreamReader(stream, Encoding.UTF8);
            //        String plainString = readStream.ReadToEnd();

            //        // Return the plain string:
            //        return plainString;
            //    }
            //}
            #endregion
        }
    }
}
