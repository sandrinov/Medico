using Me_dico.it.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string NickName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string ActivationCode { get; set; }
        public string IdMedicalCard { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public EnumUserStatus Status { get; set; }
        public EnumUserRole Role { get; set; }
        public Profile ProfileUser { get; set; }


    }
}
