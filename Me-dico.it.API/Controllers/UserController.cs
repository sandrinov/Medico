using Me_dico.it.API.Helpers;
using Me_dico.it.Repository;
using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Me_dico.it.API.Controllers
{
    public class UserController : ApiController
    {
        readonly IMedicoRepository _repository;
        readonly IFactory<User, Model.User> _uFactory;
        readonly IFactory<Profile, Model.Profile> _pFactory;
        const int maxPageSize = 8;

        public UserController(IMedicoRepository repository, 
            IFactory<User, Model.User> userFactory,
            IFactory<Profile, Model.Profile> pFactory)
        {

            //var identity = this.User as ClaimsPrincipal;

            _repository = repository;
            _uFactory = userFactory;
            _pFactory = pFactory;
        }

        [HttpGet]
        [Route("api/users", Name = "UsersList")]
        public async Task<IHttpActionResult> Get(string fields = null,
                                     string sort = "NickName",
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
                IQueryable<User> users = await _repository.GetUsersAsync(Id);

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                users = users.ApplySort(sort)
                    .Where(eg => (userId == null || eg.UserId == Id));

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = users.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new System.Web.Http.Routing.UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("UsersList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort,
                        fields = fields,
                        userId = userId
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("UsersList",
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
                return Ok(users
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(eg => _uFactory.CreateDataShapedObject(eg, lstOfFields)));

            }
            catch (Exception e)
            {
                //todo logghiamo ?
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/profile/{id}", Name = "Profile")]
        public async Task<IHttpActionResult> Get(int id, string fields = null)
        {
            try
            {
                Profile profile = await _repository.GetProfileAsync(id);

                if (profile != null)
                {
                    //return Ok(_qFactory.CreateDataShapedObject(question, lstOfFields));
                    var returnfromfactory = _pFactory.CreateDto(profile);
                    return Ok(returnfromfactory);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception excp)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/userbyemail", Name = "UserByEmail")]
        public async Task<IHttpActionResult> UserByEmail(string email, string fields = null)
        {
            try
            {
                User user = await _repository.GetUserByEmailAsync(email);

                if (user != null)
                {
                    //return Ok(_qFactory.CreateDataShapedObject(question, lstOfFields));
                    var returnfromfactory = _uFactory.CreateDto(user);
                    return Ok(returnfromfactory);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception excp)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/profilebyguid", Name = "ProfileByGuid")]
        public async Task<IHttpActionResult> GetProfileByGuid(string guid, string fields = null)
        {
            try
            {
                Profile profile = await _repository.GetProfileByGuid(guid);

                if (profile != null)
                {
                    //return Ok(_qFactory.CreateDataShapedObject(question, lstOfFields));
                    var returnfromfactory = _pFactory.CreateDto(profile);
                    return Ok(returnfromfactory);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception excp)
            {
                return Ok(excp.InnerException.Message);
            }
        }




    }
}
