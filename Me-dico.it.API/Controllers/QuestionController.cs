using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Me_dico.it.Repository;
using Me_dico.it.API.Helpers;
using System.Web;
using System.Web.Http.Routing;
using Me_dico.it.Repository.Interfaces;
using Me_dico.it.Repository.Entities;
using Thinktecture.IdentityModel.WebApi;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Diagnostics;
using Me_dico.it.API.LocalModel;
using Me_dico.it.Repository.LocalModel;

namespace Me_dico.it.API.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "GET,POST")]
    public class QuestionController : ApiController
    {
        readonly IMedicoRepository _repository;
        readonly IFactory<Question, Model.Question> _qFactory;

        const int maxPageSize = 5;


        public QuestionController(IMedicoRepository repository, IFactory<Question, Model.Question> questionFactory)
        {

            //var identity = this.User as ClaimsPrincipal;

            _repository = repository;
            _qFactory = questionFactory;
        }


        //[ResourceAuthorize("Read", "questions")]
        [Route("api/questions", Name = "QuestionsList")]
        public async Task<IHttpActionResult> Get(string fields = null,
                                     string sort = "Id",
                                     string userId = null,
                                     int page = 1,
                                     int pageSize = maxPageSize)
        {
            try
            {
                Guid userIdGuid;
                Guid? Id = null;
                if (Guid.TryParse(userId, out userIdGuid))
                {
                    Id = userIdGuid;
                }
                IQueryable<Question> questions = await _repository.GetQuestionsWithoutIncludeAsync(Id);

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                questions = questions.ApplySort(sort)
                    .Where(eg => (userId == null || eg.UserId == Id));

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = questions.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("QuestionsList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
                        userId = userId
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("QuestionsList",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
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
                return Ok(questions
                    .OrderByDescending(q=>q.UpdateDate)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(eg => _qFactory.CreateDataShapedObject(eg, lstOfFields)));

            }
            catch (Exception e)
            {
                //todo logghiamo ?
                return InternalServerError(e);
            }
        }


        [Route("api/questionsbytag", Name = "GetQuestionsByTag")]
        public async Task<IHttpActionResult> GetQuestionsByTag(int page, int tagId, string sort = "id", string fields = null, int pageSize = maxPageSize)
        {
            try
            {
                
                IQueryable<Question> questions = await _repository.GetQuestionsByTag(tagId);

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                //questions = questions.ApplySort(sort)
                //    .Where(eg => (eg.UpdateDate));

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = questions.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("GetQuestionsByTag",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
                        tagId = tagId
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("GetQuestionsByTag",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
                        tagId = tagId
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
                return Ok(questions
                    .OrderByDescending(q => q.UpdateDate)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(eg => _qFactory.CreateDataShapedObject(eg, lstOfFields)));

            }
            catch (Exception e)
            {
                //todo logghiamo ?
                return InternalServerError(e);
            }
        }



        [Route("api/questionslike", Name = "QuestionsLike")]
        public async Task<IHttpActionResult> GetQuestionsWithTextLike(string textLike,
                                    string sort = "id",
                                    string userId = null,
                                    int pageSize = maxPageSize,
                                    int page = 1, string fields = null)
        {
            try
            {
                Guid userIdGuid;
                Guid? Id = null;
                if (Guid.TryParse(userId, out userIdGuid))
                {
                    Id = userIdGuid;
                }
                IQueryable<Question> questions = await _repository.GetQuestionsLike(Id, textLike);

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                questions = questions.ApplySort(sort)
                    .Where(eg => (userId == null || eg.UserId == Id));

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = questions.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("QuestionsLike",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
                        userId = userId
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("QuestionsLike",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
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
                return Ok(questions
                    .OrderByDescending(q => q.UpdateDate)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(eg => _qFactory.CreateDataShapedObject(eg, lstOfFields)));

            }
            catch (Exception e)
            {
                //todo logghiamo ?
                return InternalServerError();
            }
        }


        [Route("api/questions/{id}", Name = "Question")]
        public async Task<IHttpActionResult> Get(int id, string fields = null)
        {
            try
            {
                List<string> lstOfFields = new List<string>();

                Question question = await Task.Run(async () =>
                {
                    if (fields != null)
                    {
                        lstOfFields = fields.ToLower().Split(',').ToList();
                    }
                    return await _repository.GetQuestionAsync(id);
                });

                if (question != null)
                {
                    //return Ok(_qFactory.CreateDataShapedObject(question, lstOfFields));
                    var returnfromfactory = _qFactory.CreateDto(question);

                    return Ok(returnfromfactory);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception excp)
            {
                return InternalServerError(excp);
            }
        }

        [HttpPost]
        [Route("api/newquestion", Name = "NewQuestion")]
        public async Task<IHttpActionResult> NewQuestion([FromBody] Question q)
        {


            var user = await _repository.GetUserByEmailAsync(q.UserName);

            /*
             * Se non si fa così, cioè se non si leggono le 
             * Entity Tags dal context, EF pensa che i Tags associati
             * alla Question q che gli arrivano dal FromBody
             * (tra l'altro correttamente deserializzati) siano delle
             * Entity Nuove e ne crea di nuovi nel DB. Questo penso sia
             * Dovuto alla relazione NxM che lega tags a Questions.
             * Così funziona. C'è un altro metodo per fare prima e 
             * non leggere dal context i tags?
             * 
             */

            List<Tag> _tags = new List<Tag>();

            foreach (var t in q.Tags)
            {
                Tag contextTag = await _repository.GetTagByIdAsync(t.Id);
                if (contextTag != null)
                    _tags.Add(contextTag);
            }
            q.Tags = _tags;

            if(user!=null)
                q.UserId = user.UserId;
            else
                q.UserId = new Guid("00000000-0000-0000-0000-000000000000");
            //to do   alert user not found

            q.UpdateDate = DateTime.Now;

            RepositoryActionResult<Question> result = _repository.AddQuestion(q);
            switch (result.Status)
            {
                case RepositoryActionStatus.Created:
                    return Content(System.Net.HttpStatusCode.OK, "Domanda inserita Correttamente");
                case RepositoryActionStatus.NothingModified:
                    return Content(System.Net.HttpStatusCode.OK, "Risultato Not Modified?"); //TODO have I to return OK?
                case RepositoryActionStatus.Error:
                    return Content(System.Net.HttpStatusCode.InternalServerError, result.Exception.InnerException.Message);
                default:
                    break;
            }


            return Content(System.Net.HttpStatusCode.Ambiguous, result.Status.ToString() + "  " + result.Entity.Title);
        }

        [HttpPost]
        [Route("api/newquestioncomment", Name = "NewQuestionComment")]
        public IHttpActionResult  NewQuestionComment([FromBody] QuestionComment qc)
        {

            //var user = await _repository.GetUserByEmailAsync(User.Identity.Name);

            //if (user != null)
            //    qc.UserId = user.UserId;
            //else
            //    qc.UserId = new Guid("00000000-0000-0000-0000-000000000000");
            //to do   alert user not found

            //qc.User = user;

            qc.UpdateDate = DateTime.Now;
            Repository.Entities.User user = _repository.GetUserByUserName(User.Identity.Name);
            if (user != null)
                qc.UserId = user.UserId;
            else
                qc.UserId = new Guid("0e5f76d4-7180-44e9-bc34-ced22a6d2bed");

            Question questionSource = _repository.GetQuestion(qc.QuestionSource.Id);

            qc.QuestionSource = questionSource;
            RepositoryActionResult<QuestionComment> result = _repository.AddQuestionComment(qc);

            switch (result.Status)
            {
                case RepositoryActionStatus.Created:
                    {
                        int newID = result.Entity.Id;
                        String UserName = result.Entity.QuestionSource.UserName;
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

        [Route("api/gettitles", Name = "GetTitles4Typehead")]
        public IHttpActionResult GetTitles4Typehead(String term)
        {
            List<TitleViewModel> titles = new List<TitleViewModel>();

            RepositoryActionResult<List<TitleViewModel>> result = _repository.GetTitles(term);
            switch (result.Status)
            {
                case RepositoryActionStatus.Ok:
                    return Content(System.Net.HttpStatusCode.OK, result.Entity);
                case RepositoryActionStatus.NotFound:
                    return Content(System.Net.HttpStatusCode.OK, titles);
                case RepositoryActionStatus.Error:
                    return Content(System.Net.HttpStatusCode.InternalServerError, result.Exception.InnerException.Message);
                default:
                    break;
            }

            return Content(System.Net.HttpStatusCode.InternalServerError, "Risultato Imprevisto");
        }
    }
}