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
    public class AnswerController : Controller
    {
        private HttpClient client = MedicoHttpClient.GetClient();

        readonly IMedicoRepository _repository;
        public AnswerController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NewAnswer(AnswerNewModel newAnswerNewModel)
        {
            newAnswerNewModel.NewAnswer.UserName = User.Identity.Name;
            var answer = JsonConvert.SerializeObject(newAnswerNewModel.NewAnswer);
            HttpResponseMessage egsResponse = await client.PostAsync("api/newanswer", new StringContent(answer, Encoding.UTF8, "application/json"));

            if (egsResponse.IsSuccessStatusCode)
            {
                var newID="";
                var UserId = "";
                var UserName = "";
                var Description = "";
                var UpdateDate = "";
                var contents = await egsResponse.Content.ReadAsStringAsync();
                if (contents.Contains("*"))
                {
                    String[] splittedContent = contents.Split(new char[] { '*' });
                    newID = splittedContent[0];
                    newID = newID.Substring(1);

                    UserId = splittedContent[1];
                    UserName = splittedContent[2];
                    Description = splittedContent[3];

                    UpdateDate = splittedContent[4];
                    UpdateDate = UpdateDate.Remove(UpdateDate.Length - 1);

                }
                return Json(new { success = true,
                    message = "OK",
                    newID = newID ,
                    UserId = UserId,
                    UserName = UserName ,
                    Description = Description ,
                    UpdateDate = UpdateDate
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Qualcosa non ha funzionato. Riprova più tardi" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NewAnswerComment(AnswerCommentNewViewModel newAnswerComment)
        {
            var answerComment = JsonConvert.SerializeObject(newAnswerComment.AnswerComment);
            HttpResponseMessage egsResponse = await client.PostAsync("api/newanswercomment", new StringContent(answerComment, Encoding.UTF8, "application/json"));

            if (egsResponse.IsSuccessStatusCode)
            {
                var newID = "";
                var UserName = "";
                var UserId = "";
                var Description = "";
                var UpdateDate = "";
                var contents = await egsResponse.Content.ReadAsStringAsync();
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
        } // end NewAnswerComment

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> VoteAnswer(AnswerVoteViewModel answerVoteModel)
        {
            var answerVote = JsonConvert.SerializeObject(answerVoteModel);
            HttpResponseMessage egsResponse = await client.PostAsync("api/newanswervote", new StringContent(answerVote, Encoding.UTF8, "application/json"));

            if (egsResponse.IsSuccessStatusCode)
            {
                var contents = await egsResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = true,
                    message = "OK"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Qualcosa non ha funzionato. Riprova più tardi" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public async Task<ActionResult> GetMyAnswers()
        {
            string userIdentityName = User.Identity.Name;
            string api_url = "api/userbyemail?email=" + userIdentityName;
            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Model.User>(content);
                string redirectUrl = "/Answer/MyAnswers?page=1&userid=" + user.UserId.ToString();

                return Redirect(redirectUrl);
            }
            else
                return Redirect("/Error/Error_500");
        }

        [Authorize]
        public async Task<ActionResult> MyAnswers(int? page, string userId)
        {
            var client = MedicoHttpClient.GetClient();

            var model = new MyAnswerViewModel();
            var api_url = "api/myanswers?page=" + page + "&userId=" + userId;

            HttpResponseMessage egsResponse = await client.GetAsync(api_url);

            if (egsResponse.IsSuccessStatusCode)
            {
                string content = await egsResponse.Content.ReadAsStringAsync();

                // get the paging info from the header
                var pagingInfo = HeaderParser.FindAndParsePagingInfo(egsResponse.Headers);

                var lstAnswers = JsonConvert.DeserializeObject<IEnumerable<Model.Answer>>(content);

                var pagedQuestionList = new StaticPagedList<Model.Answer>(lstAnswers, pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);

                //var pagedQuestionList = lstQuestions.ToPagedList(pagingInfo.CurrentPage, pagingInfo.PageSize);

                model.MyAnswers = pagedQuestionList;
                model.PagingInfo = pagingInfo;
            }
            else
            {
                return Content("An error occurred.");
            }
            ViewBag.userId = userId;
            return View(model);
        }


    }
}