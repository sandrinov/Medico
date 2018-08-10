using Me_dico.it.Models;
using Me_dico.it.Repository;
using Me_dico.it.WebClient.Helpers;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Me_dico.it.Controllers
{
    public class UserController : Controller
    {
        private HttpClient client = MedicoHttpClient.GetClient();
        private string userIdentityName;
        readonly IMedicoRepository _repository;
        public UserController(IMedicoRepository repository)
        {
            _repository = repository;
            //userIdentityName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
        }

        [Authorize]
        public async Task<ActionResult> All(int? page)
        {

            //var client = MedicoHttpClient.GetClient();

            var model = new UserViewModel();
            var api_url = "";
            if (page == null)
                api_url = "api/users";
            else
                api_url = "api/users?page=" + page;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                // get the paging info from the header
                var pagingInfo = HeaderParser.FindAndParsePagingInfo(egsResponse.Headers);

                var lstUsers = JsonConvert.DeserializeObject<IEnumerable<Model.User>>(content);

                var pagedUserList = new StaticPagedList<Model.User>(lstUsers, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);

                //var pagedQuestionList = lstQuestions.ToPagedList(pagingInfo.CurrentPage, pagingInfo.PageSize);

                model.Users = pagedUserList;
                model.PagingInfo = pagingInfo;
            }
            else
            {
                return Content("An error occurred.");
            }
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> MyProfile(int id)
        {
            var model = new ProfileViewModel();

            String api_url = "api/profile/" + id;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<Model.Profile>(content);

                string serverpath = Constants.Constants.BaseAddressMvc + "Content/images/users/";

                string photo = serverpath + profile.User.UserId.ToString() + ".png"; 

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
                    Photo = photo,
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
            userIdentityName = User.Identity.Name;

            string api_url = "api/userbyemail?email="+userIdentityName;
            Debug.WriteLine(api_url);
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