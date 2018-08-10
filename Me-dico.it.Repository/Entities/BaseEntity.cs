using System;
using System.ComponentModel.DataAnnotations;

namespace Me_dico.it.Repository.Entities
{
    public class BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}