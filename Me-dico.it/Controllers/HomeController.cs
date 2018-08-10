using IdentityServerRepository;
using Me_dico.it.Models;
using Me_dico.it.Repository;
using Me_dico.it.WebClient.Helpers;
using Newtonsoft.Json;
using PagedList;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace Me_dico.it.Controllers
{
    public class HomeController : Controller
    {
        readonly IMedicoRepository _repository;
        public HomeController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [ResourceAuthorize("Read", "questions")]
        public async Task<ActionResult> Index(int? page)
        {

            var client = MedicoHttpClient.GetClient();
            //var client = new HttpClient();
            //client.BaseAddress = new System.Uri("http://localhost:2627/");


            var model = new QuestionViewModel();
            var api_url = "";
            if (page == null)
                api_url = "api/questions";
            else
                api_url = "api/questions?page=" + page;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                // get the paging info from the header
                var pagingInfo = HeaderParser.FindAndParsePagingInfo(egsResponse.Headers);

                var lstQuestions = JsonConvert.DeserializeObject<IEnumerable<Model.Question>>(content);

                var pagedQuestionList = new StaticPagedList<Model.Question>(lstQuestions, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);

                //var pagedQuestionList = lstQuestions.ToPagedList(pagingInfo.CurrentPage, pagingInfo.PageSize);

                model.Questions = pagedQuestionList;
                model.PagingInfo = pagingInfo;
            }
            else
            {
                return Content("An error occurred.");
            }

            ViewBag.Id = 11;
            return View(model);
        }


        [Authorize]
        public async Task<ActionResult> IndexWithSearch(string textLike, int? page)
        {

            var client = MedicoHttpClient.GetClient();
            //var client = new HttpClient();
            //client.BaseAddress = new System.Uri("http://localhost:2627/");


            var model = new QuestionViewModel();

            string _textlike = textLike == null ? "" : textLike;
            var api_url = "";
            if (page == null)
                api_url = "api/questionslike?textlike="+ _textlike;
            else
                api_url = "api/questionslike?textlike=" + _textlike +"&page=" + page;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                // get the paging info from the header
                var pagingInfo = HeaderParser.FindAndParsePagingInfo(egsResponse.Headers);

                var lstQuestions = JsonConvert.DeserializeObject<IEnumerable<Model.Question>>(content);

                var pagedQuestionList = new StaticPagedList<Model.Question>(lstQuestions, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);

                //var pagedQuestionList = lstQuestions.ToPagedList(pagingInfo.CurrentPage, pagingInfo.PageSize);

                model.Questions = pagedQuestionList;
                model.PagingInfo = pagingInfo;
            }
            else
            {
                return Content("An error occurred.");
            }

            ViewBag.TextLike = textLike;
            return View(model);
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Required authentications...";
            return View();
        }

        [AllowAnonymous]
        public ActionResult LandingPage()
        {
            ViewBag.Registrazione = false;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Istruzioni()
        {
            ViewBag.Registrazione = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LandingPage(Model.User user)
        {
            if (user.Password != user.PasswordCheck)
                ModelState.AddModelError("PasswordCheck", "Verifica password non corretta");

            if (ModelState.IsValid)
            {
                // Verify that user not exist yet
                var tmpUser = await _repository.GetUserByEmailAsync(user.Email);
                // Setting User Role
                user.Role = Constants.EnumUserRole.User;

                //  controllo codice attivazione  
                var canRegister = await _repository.CanRegister(user.ActivationCode);

                // This must be in transaction
                if (tmpUser == null)
                {
                    if (canRegister)
                    {
                        // There is a promotable enlistment for the transaction which has a PromoterType value that is not recognized by System.Transactions.
                        // using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        var u = await _repository.AddUserAsync(user);
                        using (var ctx = new IdentityRepository(new IdentityContext()))
                        {
                            ctx.AddUser(new IdentityServerRepository.Model.CustomUser
                            {
                                Username = u.Email,
                                Password = u.Password,
                                Role = u.Role
                            });
                        }
                        //ts.Complete();
                        user = new Model.User();
                    }
                    else
                    {
                        ModelState.AddModelError("ActivactionCode", "Codice di attivazione non corretto.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Utente già registrato");
                }
            }
            ViewBag.Registrazione = true;
            return View(user);
        }

        [Authorize]
        [ResourceAuthorize("Read", "questions")]
        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Calling web api";


            var client = MedicoHttpClient.GetClient();

            var model = new QuestionViewModel();

            HttpResponseMessage egsResponse = await client.GetAsync("api/questions");

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                // get the paging info from the header
                var pagingInfo = HeaderParser.FindAndParsePagingInfo(egsResponse.Headers);

                var lstQuestions = JsonConvert.DeserializeObject<IEnumerable<Model.Question>>(content);

                var pagedQuestionList = new StaticPagedList<Model.Question>(lstQuestions, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);

                model.Questions = pagedQuestionList;
                model.PagingInfo = pagingInfo;
            }
            else
            {
                return Content("An error occurred.");
            }

            return View(model);
        }
    }
}