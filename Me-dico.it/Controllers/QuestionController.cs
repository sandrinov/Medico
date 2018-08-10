using Me_dico.it.Model;
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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Me_dico.it.Controllers
{
    public class QuestionController : Controller
    {
        private HttpClient client = MedicoHttpClient.GetClient();

        readonly IMedicoRepository _repository;
        public QuestionController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        // GET: Question
        [Authorize]
        public async Task<ActionResult> GetQuestion(int id)
        {
            var model = new QuestionResponsesViewModel();
            HttpResponseMessage egsResponse = await client.GetAsync("api/questions/"+ id);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                //// get the paging info from the header
                //var pagingInfo = HeaderParser.FindAndParsePagingInfo(egsResponse.Headers);

                var questionResult = JsonConvert.DeserializeObject<Model.Question>(content);

                //    var pagedQuestionList = new StaticPagedList<Model.Question>(lstQuestions, pagingInfo.CurrentPage,
                //        pagingInfo.PageSize, pagingInfo.TotalCount);

                //    model.Questions = pagedQuestionList;
                //    model.PagingInfo = pagingInfo;
                model.Question = questionResult;
            }
            else
            {
                return Redirect("/Error/Error_500");
            }
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> GetQuestionsByTag(int page, int tagId, string tagName)
        {

            var client = MedicoHttpClient.GetClient();
          
            var model = new QuestionViewModel();

            var api_url = "api/questionsbytag?page=" + page + "&tagId=" + tagId;

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

            ViewBag.tagId = tagId;
            ViewBag.tagName = tagName;
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> GetMyQuestions()
        {

            string userIdentityName = User.Identity.Name;
            string api_url = "api/userbyemail?email=" + userIdentityName;
            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Model.User>(content);
                string redirectUrl = "/Question/MyQuestions?page=1&userid=" + user.UserId.ToString();

                return Redirect(redirectUrl);
            }
            else
                return Redirect("/Error/Error_500");
        }

        [Authorize]
        public async Task<ActionResult> MyQuestions(int? page, string userId)
        {
            var client = MedicoHttpClient.GetClient();
            //var client = new HttpClient();
            //client.BaseAddress = new System.Uri("http://localhost:2627/");

            var model = new QuestionViewModel();
            var api_url = "api/questions?page=" + page + "&userId=" + userId;

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
            ViewBag.userId = userId;
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult NewQuestion()
        {
            //QuestionNewModel q = new QuestionNewModel();
            //q.Question = new Question();
            //return View(q);
            return View();
        }
      
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NewQuestion(QuestionNewModel newQuestion)
        {
            ///TODO
            /// make here controls for validation!!!!
            //Debug.WriteLine(User.Identity.Name);
            //Console.WriteLine(User.Identity.Name);
            newQuestion.Question.UserName = User.Identity.Name;

            var question = JsonConvert.SerializeObject(newQuestion.Question);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(question);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage egsResponse = await client.PostAsync("api/newquestion", new StringContent(question, Encoding.UTF8, "application/json"));
            //HttpContent contentPost = new StringContent(question, Encoding.UTF8, "application/json");


            //HttpResponseMessage egsResponse = await client.PostAsync(new Uri("api/newquestion"), contentPost);


            if (egsResponse.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Domanda inserita correttamente" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Qualcosa non ha funzionato. Riprova più tardi" }, JsonRequestBehavior.AllowGet);
            }


        } // end NewQuestion

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NewQuestionComment(QuestionCommentNewViewModel newQuestionComment)
        {

            newQuestionComment.QuestionComment.UserName = User.Identity.Name;
          
            var questionComment = JsonConvert.SerializeObject(newQuestionComment.QuestionComment);
            HttpResponseMessage egsResponse = await client.PostAsync("api/newquestioncomment", new StringContent(questionComment, Encoding.UTF8, "application/json"));

            if (egsResponse.IsSuccessStatusCode)
            {
                var newID = "";
                var UserName = "";
                var UserId = "";
                var Description = "";
                var UpdateDate = "";
                var contents = await egsResponse.Content.ReadAsStringAsync();
                //var contents = "\"13 * *0e5f76d4 - 7180 - 44e9 - bc34 - ced22a6d2bed * Commento dioboninooooooooo * 9 / 19 / 2016\"";
                if (contents.Contains("*"))
                {
                    String[] splittedContent = contents.Split(new char[] { '*' });
                    newID = splittedContent[0];
                    newID = newID.Substring(1);

                    UserName = splittedContent[1];
                    UserId = splittedContent[2];
                    Description = splittedContent[3];

                    UpdateDate = splittedContent[4];
                    UpdateDate = UpdateDate.Remove(UpdateDate.Length - 1);

                }
                return Json(new
                {
                    success = true,
                    message = "OK",
                    newID = newID,
                    UserName = UserName,
                    UserId = UserId,
                    Description = Description,
                    UpdateDate = UpdateDate
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Qualcosa non ha funzionato. Riprova più tardi" }, JsonRequestBehavior.AllowGet);
            }
        } // end NewQuestionComment

    }// end Question Controller
}