using Me_dico.it.Models;
using Me_dico.it.Repository;
using Me_dico.it.WebClient.Helpers;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Me_dico.it.Controllers
{
    public class StatisticheController : Controller
    {
        private HttpClient client = MedicoHttpClient.GetClient();

        readonly IMedicoRepository _repository;
        public StatisticheController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> MyProfile(int id)
        {
            var client = MedicoHttpClient.GetClient();

            var model = new ProfileViewModel();

            String api_url = "api/profile/" + id;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<Model.Profile>(content);

                string lastLoginDate = profile.User.LastLoginDate.HasValue ? profile.User.LastLoginDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                string dateOfBirth = profile.DateOfBirth.HasValue ? profile.DateOfBirth.Value.ToShortDateString() : "Non Dichiarato";
                string gender = "";
                switch (profile.Gender)
                {
                    case Constants.EnumGender.Male:
                        gender = "M";
                        break;
                    case Constants.EnumGender.Female:
                        gender = "F";
                        break;
                    case Constants.EnumGender.NonDefinito:
                        gender = "?";
                        break;
                    default:
                        break;
                }
                string role = "";
                switch (profile.User.Role)
                {
                    case Constants.EnumUserRole.SuperAdmin:
                        role = "SuperAdmin";
                        break;
                    case Constants.EnumUserRole.Admin:
                        role = "Admin";
                        break;
                    case Constants.EnumUserRole.User:
                        role = "User";
                        break;
                    default:
                        break;
                }
                string status = "";
                switch (profile.User.Status)
                {
                    case Constants.EnumUserStatus.Actived:
                        status = "Attivato";
                        break;
                    case Constants.EnumUserStatus.Disabled:
                        status = "Disabilitato";
                        break;
                    case Constants.EnumUserStatus.WaitingActivaction:
                        status = "In attesa di Attivazione";
                        break;
                    default:
                        break;
                }

                ProfileViewModel vm = new ProfileViewModel()
                {
                    Id = profile.Id,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Title = profile.Title,
                    Photo = profile.Photo,
                    Address1 = profile.Address1,
                    Address2 = profile.Address1,
                    City = profile.City,
                    Country = profile.Country,
                    Mobile = profile.Mobile,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    CreateDate = profile.User.CreateDate.ToShortDateString(),
                    Email = profile.User.Email,
                    LastLoginDate = lastLoginDate,
                    NickName = profile.User.NickName,
                    Role = role,
                    Status = status

                };
                model = vm;
            }
            else
            {
                return Content("An error occurred.");
            }
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> GetMyProfile()
        {
            var client = MedicoHttpClient.GetClient();

            string userIdentityName = User.Identity.Name;
            string api_url = "api/userbyemail?email="+ userIdentityName;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);
            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Model.User>(content);
                string redirectUrl = "/User/MyProfile/" + user.ProfileUser.Id;

                return Redirect(redirectUrl);
            }
            else
                return Redirect("/Error/Error_500");
        }

    }
}