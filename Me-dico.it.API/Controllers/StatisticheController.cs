using Me_dico.it.API.Helpers;
using Me_dico.it.Repository;
using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using Me_dico.it.Repository.LocalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Thinktecture.IdentityModel.WebApi;

namespace Me_dico.it.API.Controllers
{
    public class StatisticheController : ApiController
    {
        readonly IMedicoRepository _repository;

        public StatisticheController(IMedicoRepository repository)
        {

            _repository = repository;

        }

        [HttpGet]
        //[ResourceAuthorize("Read", "questions")]
        [Route("api/getstatistiche", Name = "GetStatistiche")]
        public IHttpActionResult GetStatistiche(string userName)
        {
            List<StatisticsDataViewModel> titles = new List<StatisticsDataViewModel>();

            RepositoryActionResult<List<StatisticsDataViewModel>> result = _repository.GetStatistics(User.Identity.Name);
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
