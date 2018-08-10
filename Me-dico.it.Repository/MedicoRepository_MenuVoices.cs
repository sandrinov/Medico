using Me_dico.it.Repository.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository
    {
        #region Async
        public async Task<IQueryable<MenuVoice>> GetMenuVoicesAsync()
        {
            return await Task.Run(() =>
            {
                return _ctx.MenusVoices;
            });
        }

        #endregion
    }
}
