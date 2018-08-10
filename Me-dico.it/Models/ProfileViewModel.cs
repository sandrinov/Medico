using Me_dico.it.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me_dico.it.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public String CreateDate { get; set; }
        public String LastLoginDate { get; set; }
        public String Status { get; set; }
        public String Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public String DateOfBirth { get; set; }
        public String Gender { get; set; }
    }
}