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
    public class TagController : ApiController
    {
        readonly IMedicoRepository _repository;
        readonly IFactory<Tag, Model.Tag> _tFactory;

        public TagController(IMedicoRepository repository, IFactory<Tag, Model.Tag> tagFactory)
        {

            //var identity = this.User as ClaimsPrincipal;

            _repository = repository;
            _tFactory = tagFactory;
        }

        [Route("api/tagslist", Name = "TagsList")]
        public async Task<IHttpActionResult> Get(string fields = null)
        {
            try
            {

                IQueryable<Tag> tags = await _repository.GetTagsAsync();

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }
                // return result
                // return result
                return Ok(tags.ToList()
                    .Select(eg => _tFactory.CreateDataShapedObject(eg, lstOfFields)));

            }
            catch (Exception e)
            {
                //todo logghiamo ?
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/newtag", Name = "NewTag")]
        public IHttpActionResult NewTag(Tag t)
        {
            var user = _repository.GetUserByUserName(User.Identity.Name);

            if (user != null)
                t.UserId = user.UserId;
            else
                t.UserId = new Guid("0e5f76d4-7180-44e9-bc34-ced22a6d2bed");
            t.UpdateDate = DateTime.Now;

            RepositoryActionResult<Tag> result = _repository.AddTag(t);
            switch (result.Status)
            {
                case RepositoryActionStatus.Created: 
                    return Content(System.Net.HttpStatusCode.OK, new
                    {
                        success = true,
                        newID = result.Entity.Id,
                        Description = result.Entity.Description 
                    });
                case RepositoryActionStatus.NothingModified:
                    return Content(System.Net.HttpStatusCode.OK, "Risultato Not Modified?"); //TODO have I to return OK?
                case RepositoryActionStatus.Error:
                    return Content(System.Net.HttpStatusCode.InternalServerError, result.Exception.InnerException.Message);
                default:
                    break;
            }


            return Content(System.Net.HttpStatusCode.Ambiguous, result.Status.ToString() + " Per il Tag:  " + result.Entity.Description);
        }



    }
}
