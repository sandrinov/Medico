using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("Tag")]
    public class Tag : BaseEntity
    {
        public Tag()
        {
        }
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
