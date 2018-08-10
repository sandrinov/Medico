using Me_dico.it.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Me_dico.it.Model
{
    public class Profile : BaseDTO
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "Il Nome deve essere impostato.")]
        [Display(Description="Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Il Cognome deve essere impostato.")]
        [Display(Description = "Cognome")]
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public EnumGender Gender { get; set; }
    }
}
