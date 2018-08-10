using Me_dico.it.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Me_dico.it.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public string NickName { get; set; }

        [Required(ErrorMessage = "La email deve essere impostata.")]
        [EmailAddress(ErrorMessage ="E-mail non è valida")]
        [Display(Description = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Password deve essere impostata.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Minimo 5 caratteri richiesti")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; }

        [Required(ErrorMessage = "Il codice di attivazione deve essere impostato.")]
        public string ActivationCode { get; set; }
        public string IdMedicalCard { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public EnumUserStatus Status { get; set; }
        public EnumUserRole Role { get; set; }
        public Profile ProfileUser { get; set; }

    }
}