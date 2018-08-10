using IdentityServerRepository;
using IdentityServerRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerRepository
{
    public class IdentityRepository : IIdentityRepository, IDisposable
    {
        readonly IdentityContext _ctx;
        public IdentityRepository(IdentityContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public CustomUser GetUser(string userName, string password)
        {
            return _ctx.CustomUsers.Where(e => e.Username == userName && e.Password == password).SingleOrDefault();
        }
        public async Task<CustomUser> GetUserAsync(string userName, string password)
        {
            return await Task.Run(() => _ctx.CustomUsers.Where(e => e.Username == userName && e.Password == password).SingleOrDefault());
        }

        public CustomUser GetUserId(string subject)
        {
            return _ctx.CustomUsers.Single(e => e.Subject.ToString() == subject);
        }
        public async Task<CustomUser> GetUserIdAsync(string subject)
        {
            return await _ctx.CustomUsers.FindAsync(subject);
        }

        public RepositoryActionResult<CustomUser> AddUser(CustomUser customUser )
        {
            try
            {
                _ctx.CustomUsers.Add(customUser);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<CustomUser>(customUser, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<CustomUser>(customUser, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<CustomUser>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public async Task<RepositoryActionResult<CustomUser>> AddUserAsync(CustomUser customUser)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _ctx.CustomUsers.Add(customUser);
                    var result = _ctx.SaveChanges();
                    if (result > 0)
                    {
                        return new RepositoryActionResult<CustomUser>(customUser, RepositoryActionStatus.Created);
                    }
                    else
                    {
                        return new RepositoryActionResult<CustomUser>(customUser, RepositoryActionStatus.NothingModified, null);
                    }

                }
                catch (Exception ex)
                {
                    return new RepositoryActionResult<CustomUser>(null, RepositoryActionStatus.Error, ex);
                }
            });
        }
    }
}
