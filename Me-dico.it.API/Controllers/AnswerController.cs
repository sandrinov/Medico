using Me_dico.it.Repository;
using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace Me_dico.it.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET,POST")]
    public class AnswerController : ApiController
    {
        readonly IMedicoRepository _repository;
        readonly IFactory<Answer, Model.Answer> _aFactory;
        const int maxPageSize = 5;

        public AnswerController(IMedicoRepository repository, IFactory<Answer, Model.Answer> answerFactory)
        {

            //var identity = this.User as ClaimsPrincipal;

            _repository = repository;
            _aFactory = answerFactory;
        }

        [HttpPost]
        [Route("api/newanswer", Name = "NewAnswer")]
        public async Task<IHttpActionResult> NewAnswer([FromBody] Answer a)
        {
            var user = await _repository.GetUserByEmailAsync(a.UserName);
            if (user != null)
                a.UserId = user.UserId;
            else
                a.UserId = new Guid("00000000-0000-0000-0000-000000000000");
            //to do   alert user not found
            a.UpdateDate = DateTime.Now;

            Question questionSource = _repository.GetQuestion(a.QuestionSource.Id);

            ////aaa
            questionSource.AnswersCount++;

            a.QuestionSource = questionSource;
            RepositoryActionResult<Answer> result = _repository.AddAnswer(a);
            ///
            switch (result.Status)
            {
                case RepositoryActionStatus.Created:
                    {
                        int newID = result.Entity.Id;
                        String UserId = result.Entity.UserId.ToString();
                        String UserName = result.Entity.UserName;
                        String Description = result.Entity.Description;
                        String UpdateDate = result.Entity.UpdateDate.HasValue ? result.Entity.UpdateDate.Value.ToShortDateString() : "";
                        return Content(System.Net.HttpStatusCode.OK, newID + "*" + UserId + "*" + UserName + "*" + Description + "*" + UpdateDate);
                    }
                case RepositoryActionStatus.NothingModified:
                    return Content(System.Net.HttpStatusCode.OK, "Risultato Not Modified?"); //TODO have I to return OK?
                case RepositoryActionStatus.Error:
                    return Content(System.Net.HttpStatusCode.InternalServerError, result.Exception.InnerException.Message);
                default:
                    break;
            }
            return Content(System.Net.HttpStatusCode.Ambiguous, result.Status.ToString() + "  " + result.Entity.Description);
        }

        [HttpPost]
        [Route("api/newanswercomment", Name = "NewAnswerComment")]
        public IHttpActionResult NewAnswerComment([FromBody] AnswerComment ac)
        {
            ac.UpdateDate = DateTime.Now;
            Repository.Entities.User user = _repository.GetUserByUserName(User.Identity.Name);
            if (user != null)
            {
                ac.UserId = user.UserId;
                ac.NickName = user.NickName;
            }
            else
                ac.UserId = new Guid("0e5f76d4-7180-44e9-bc34-ced22a6d2bed");

            Answer answerSource = _repository.GetAnswer(ac.AnswerSource.Id);

            ac.AnswerSource = answerSource;
            RepositoryActionResult<AnswerComment> result = _repository.AddAnswerComment(ac);

            switch (result.Status)
            {
                case RepositoryActionStatus.Created:
                    {
                        int newID = result.Entity.Id;
                        String UserName = result.Entity.AnswerSource.UserName;
                        String UserId = result.Entity.UserId.ToString();
                        String Description = result.Entity.Description;
                        String UpdateDate = result.Entity.UpdateDate.HasValue ? result.Entity.UpdateDate.Value.ToShortDateString() : "";
                        return Content(System.Net.HttpStatusCode.OK, newID + "*" + UserName + "*" + UserId + "*" + Description + "*" + UpdateDate);
                    }
                case RepositoryActionStatus.NothingModified:
                    return Content(System.Net.HttpStatusCode.OK, "Risultato Not Modified?"); //TODO have I to return OK?
                case RepositoryActionStatus.Error:
                    return Content(System.Net.HttpStatusCode.InternalServerError, result.Exception.InnerException.Message);
                default:
                    break;
            }
            return Content(System.Net.HttpStatusCode.Ambiguous, result.Status.ToString() + "  " + result.Entity.Description);
        }

        [HttpPost]
        [Route("api/newanswervote", Name = "AnswerVote")]
        public IHttpActionResult AnswerVote([FromBody] Model.AnswerVote av)
        {
            Answer a = _repository.GetAnswer(av.AnswerId);
            if (av.Vote == "+")
                a.VoteCount++;
            else
                a.VoteCount--;
            a.UpdateDate = DateTime.Now;

            RepositoryActionResult<Answer> result = _repository.UpdateAnswer(a);

            switch (result.Status)
            {
                case RepositoryActionStatus.Updated:
                    return Content(System.Net.HttpStatusCode.OK, "OK");
                default:
                    return Content(System.Net.HttpStatusCode.InternalServerError, "KO");
            }
        }

        [HttpGet]
        [Route("api/myanswers", Name ="MyAnswers")]
        public async Task<IHttpActionResult> MyAnswers(int page, string userId)
        {
            Guid _userId;
            Guid.TryParse(userId, out _userId);
            int pageSize = maxPageSize;

            if (_userId != null)
            {
                try
                {
                    IQueryable<Answer> answers = await _repository.MyAnswers(_userId);

                    List<string> lstOfFields = new List<string>();

                    // calculate data for metadata
                    var totalCount = answers.Count();
                    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                    var urlHelper = new UrlHelper(Request);
                    var prevLink = page > 1 ? urlHelper.Link("MyAnswers",
                        new
                        {
                            page = page - 1,
                            userId = userId
                        }) : "";
                    var nextLink = page < totalPages ? urlHelper.Link("MyAnswers",
                        new
                        {
                            page = page + 1,
                            userId = userId
                        }) : "";


                    var paginationHeader = new
                    {
                        currentPage = page,
                        pageSize = pageSize,
                        totalCount = totalCount,
                        totalPages = totalPages,
                        previousPageLink = prevLink,
                        nextPageLink = nextLink
                    };

                    HttpContext.Current.Response.Headers.Add("X-Pagination",
                       Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


                    // return result
                    return Ok(answers
                        .OrderByDescending(a=> a.UpdateDate)
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList()
                        .Select(eg => _aFactory.CreateDataShapedObject(eg, lstOfFields)));
                }
                catch (Exception e)
                {
                    //todo logghiamo ?
                    return InternalServerError(e);
                }
            }
            else
                return Content(System.Net.HttpStatusCode.BadRequest, "KO");
        }
    }
}
