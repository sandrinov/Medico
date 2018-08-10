using IdentityServerRepository;
using Me_dico.it.Models;
using Me_dico.it.Repository;
using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using Me_dico.it.WebClient.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace Me_dico.it.Controllers
{
    public class MenuController : AsyncController
    {
        readonly IMedicoRepository _repository;
        public MenuController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [ResourceAuthorize("Read", "questions")]
        public async Task<ActionResult> Index()
        {

            var client = MedicoHttpClient.GetClient();

            var model = new MenuViewModel();

            HttpResponseMessage egsResponse = await client.GetAsync("api/menuvoices");

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                var lstMenuVoices = JsonConvert.DeserializeObject<IEnumerable<Model.MenuVoice>>(content);

                model.MenuVoices = lstMenuVoices;
                model.Avatar = null;
                model.Health = 100;
                model.Reputation = 100;
                model.UserName = "pippo";
            }
            else
            {
                return Content("An error occurred.");
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return PartialView(model);
        }
    }
}
