using System;
using System.ComponentModel.DataAnnotations;

namespace Me_dico.it.Repository.Entities
{
    public class BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}