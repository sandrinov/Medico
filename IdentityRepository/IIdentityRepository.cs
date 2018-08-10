using IdentityServerRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerRepository
{
    public interface IIdentityRepository
    {
        CustomUser GetUser(string userName, string password);
        Task<CustomUser> GetUserAsync(string userName, string password);

        CustomUser GetUserId(string subject);
        Task<CustomUser> GetUserIdAsync(string subject);

        RepositoryActionResult<CustomUser> AddUser(CustomUser customUser);
        Task<RepositoryActionResult<CustomUser>> AddUserAsync(CustomUser customUser);
    }
}
