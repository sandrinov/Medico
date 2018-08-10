using Me_dico.it.Repository.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository
    {
       

        #region Async operations

        public async Task<IQueryable<Tag>> GetTagsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Tags.OrderBy(t=>t.Description);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }

        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Tags.FirstOrDefault(t => t.Id == tagId);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }

        public RepositoryActionResult<Tag> AddTag(Tag t)
        {
            #region try
            try
            {
                _ctx.Tags.Add(t);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Tag>(t, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Tag>(t, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Tag>(null, RepositoryActionStatus.Error, ex);
            }
            #endregion
        }

        #endregion
    }
}
