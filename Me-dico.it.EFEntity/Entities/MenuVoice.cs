using Me_dico.it.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("Menu")]
    public class MenuVoice : BaseEntity
    {
        public int Id { get; set; }
        public String IconName { get; set; }
        public String Text { get; set; }
        public String Controller { get; set; }
        public String Action { get; set; }
        public EnumUserRole Role { get; set; }
        public EnumUserStatus UserStatus { get; set; }
    }
}
