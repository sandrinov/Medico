using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("AnswerComment")]
    public class AnswerComment : BaseEntity
    {
        public int Id { get; set; }
        //public virtual User User { get; set; }
        public string NickName { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual Answer AnswerSource { get; set; }
    }
}
