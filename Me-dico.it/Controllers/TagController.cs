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
    public class TagController : Controller
    {
        private HttpClient client = MedicoHttpClient.GetClient();

        readonly IMedicoRepository _repository;
        public TagController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public async Task<ActionResult> All()
        {

            var client = MedicoHttpClient.GetClient();

            var model = new TagViewModel();
            String api_url = "api/tagslist";

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                var lstTags = JsonConvert.DeserializeObject<IEnumerable<Model.Tag>>(content);
                model.Tags = lstTags;
            }
            else
            {
               
                return Content(egsResponse.StatusCode.ToString());
            }
            return View(model);
        }

    }
}