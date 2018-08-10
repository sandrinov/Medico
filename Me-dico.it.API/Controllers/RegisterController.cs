using System;
using System.Linq;
using System.Web.Http;
using Me_dico.it.Repository;
using Me_dico.it.Repository.Interfaces;
using Me_dico.it.Repository.Entities;
using System.Threading.Tasks;

namespace Me_dico.it.API.Controllers
{
    public class RegisterController : ApiController
    {
        readonly IMedicoRepository _repository;
        const int maxPageSize = 10;


        public RegisterController(IMedicoRepository repository)
        {
            //var identity = this.User as ClaimsPrincipal;

            _repository = repository;
        }
        [Route("api/canregister", Name = "CanRegister")]
        [HttpPost]
        public async Task<IHttpActionResult> Get(LocalModel.RegistrationData data)
        {
            try {
                ActivationCode act = new ActivationCode
                {
                    MedicoCode = data.MedicoCode,
                    Mobile = data.PhoneNumber,
                    CreateDate = DateTime.Parse(data.RegistrationTime),
                    ActivactionCode = data.ActivationCode,
                    ExpiredDate = DateTime.Parse(data.RegistrationTime).AddMinutes(10)
                };
                ActivationCode activaction = await _repository.TryRegisterAsync(act);
                string output =  (activaction == null) ? "Ok" : "No";
                return Json(new { success = output });
            }
            catch (Exception e)
            {
                //todo logghiamo ?
                string cacca = e.Message;
                string caccadicacca = e.InnerException.Message;
                return InternalServerError();
            }
        }
    }
}