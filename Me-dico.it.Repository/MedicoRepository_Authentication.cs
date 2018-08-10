using Me_dico.it.Repository.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository
    {
        public async Task<bool> CanRegister(string ActivactionCode)
        {
            return await Task.Run(() =>
            {
                ActivationCode act = _ctx.ActivationCodes.FirstOrDefault(e => (e.ActivactionCode == ActivactionCode));
                if (act == null || act.ExpiredDate < DateTime.Now)
                {
                    return false;
                }
                return true;
            });
        }
        public async Task<ActivationCode> TryRegisterAsync(ActivationCode act)
        {
            return await Task.Run(() =>
            {
                ActivationCode rec = _ctx.ActivationCodes.FirstOrDefault(e => (e.Mobile == act.Mobile) || (e.MedicoCode == act.MedicoCode));
                if (rec == null)
                {
                    _ctx.ActivationCodes.Add(act);
                    _ctx.SaveChanges();
                }
                return rec;
            });
        }

    }
}
