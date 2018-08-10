using Me_dico.it.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Model
{
    public class MenuVoice : BaseDTO
    {
        public int Id { get; set; }
        public string IconName { get; set; }
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public EnumUserRole Role { get; set; }   
        public EnumUserStatus UserStatus { get; set; }
    }
}
