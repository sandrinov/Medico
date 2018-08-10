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
using System.Web.Http;
using System.Web.Http.Cors;

namespace Me_dico.it.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET,POST")]
    public class MenuController : ApiController
    {
        readonly IMedicoRepository _repository;
        readonly IFactory<MenuVoice, Model.MenuVoice> _qMenu;
        public MenuController(IMedicoRepository repository, IFactory<MenuVoice, Model.MenuVoice> menuFactory)
        {
            _repository = repository;
            _qMenu = menuFactory;
        }
        [AllowAnonymous]
        [Route("api/menuvoices", Name = "MenuList")]
        public async Task<IHttpActionResult> Get(string fields = null, string sort = "id")
        {
            try
            {
                IQueryable<MenuVoice> menu = await _repository.GetMenuVoicesAsync();

                List<string> lstOfFields = new List<string>();

                menu = menu.ApplySort(sort);

                // return result
                return Ok(menu
                    .ToList()
                    .Select(eg => _qMenu.CreateDto(eg)));
            }
            catch (Exception e)
            {
                //todo logghiamo ?
                return InternalServerError();
            }
        }
    }
}
