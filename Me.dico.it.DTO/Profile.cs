using Me_dico.it.Constants;
using System;
namespace Me.dico.it.DTO
{
    public class Profile : BaseDTO
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EnumGender Gender { get; set; }
    }
}
