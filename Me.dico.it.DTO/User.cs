using Me_dico.it.Constants;
using System;

namespace Me.dico.it.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }

        public string ActivationCode { get; set; }
        public string IdMedicalCard { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public EnumUserStatus Status { get; set; }
        public EnumUserRole Role { get; set; }
        public Profile ProfileUser { get; set; }

    }
}