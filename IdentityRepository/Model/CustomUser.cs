using Me_dico.it.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerRepository.Model
{
    public class CustomUser
    {
        public CustomUser()
        {
        }
        [Key]
        public Guid Subject { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public EnumUserRole Role { get; set; }

        [NotMapped]
        public List<Claim> Claims { get; set; }
    }
}
