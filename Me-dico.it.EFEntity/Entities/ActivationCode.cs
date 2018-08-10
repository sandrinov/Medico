using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("ActivationCode")]
    public class ActivationCode
    {
        [Key]
        public int ActivationCodeId { get; set; }
        [Required]
        public string MedicoCode { get; set; }
        [Required]
        public string ActivactionCode { get; set; }

        [Required]
        public string Mobile { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public Byte[] Image { get; set; }
    }
}
