using Me_dico.it.Repository.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository
    {
        public User GetUserByUserName(string userName)
        {
            return _ctx.Users.Where(u => u.Email == userName).FirstOrDefault();
        }
        public User AddUser(Model.User user)
        {
            try
            {
                var userToAdd = new User
                {
                    CreateDate = DateTime.Now,

                    Email = user.Email,
                    Password = user.Password,
                    ProfileUser = new Profile
                    {
                        FirstName = user.ProfileUser.FirstName,
                        LastName = user.ProfileUser.LastName,
                        Gender = Constants.EnumGender.NonDefinito
                    }
                };
                _ctx.Users.Add(userToAdd);
                _ctx.Entry(userToAdd).State = EntityState.Added;
                _ctx.SaveChanges();
                return userToAdd;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
        public User GetUserById(Guid userId)
        {
            return _ctx.Users.FirstOrDefault(e => e.UserId == userId);
        }

        #region Async operations
        public async Task<User> AddUserAsync(Model.User user)
        {
            try
            {
                var userToAdd = new User
                {
                    CreateDate = DateTime.Now,

                    Email = user.Email,
                    Password = user.Password,
                    ProfileUser = new Profile
                    {
                        FirstName = user.ProfileUser.FirstName,
                        LastName = user.ProfileUser.LastName,
                        Gender = Constants.EnumGender.NonDefinito
                    }
                };
                _ctx.Users.Add(userToAdd);
                _ctx.Entry(userToAdd).State = EntityState.Added;
                await _ctx.SaveChangesAsync().ConfigureAwait(false);
                return userToAdd;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await Task.Run(() =>
            {
                //var result = from u in _ctx.Users.Include("Profiles")
                //             where u.Email == email
                //             select u;
                //var user = result.FirstOrDefault();
                ////if (user != null)
                ////{
                ////    var res = from p in _ctx.Profiles
                ////              where p.User.UserId == user.UserId
                ////              select p;

                ////    var prof = res.FirstOrDefault();

                ////}

                return _ctx.Users.Include(u => u.ProfileUser).FirstOrDefault(e => e.Email == email);
            });
        }
        public async Task<IQueryable<User>> GetUsersWithoutIncludeAsync(Guid? userId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Users
                        .Where(u => (userId == null || u.UserId == userId));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        public async Task<IQueryable<User>> GetUsersAsync(Guid? userId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Users
                    //.Include(u => u.ProfileUser)
                    .Where(u => (userId == null || u.UserId == userId));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        public async Task<Profile> GetProfileAsync(int id)
        {
            return await Task.Run(() =>
            {
                return _ctx.Profiles.Include(p=>p.User).FirstOrDefault(p => p.Id == id);
            });
        }
        public async Task<Profile> GetProfileByGuid(string guid)
        {
            Guid g = new Guid(guid);

            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Profiles.FirstOrDefault(p => p.User.UserId == g);
                }
                catch (Exception e)
                {
                    return new Profile() { Address1 = e.InnerException.Message };
                }
            });
        }
        #endregion
    }
}
