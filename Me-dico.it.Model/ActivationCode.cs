using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Model
{

    public class ActivationCode
    {
        public Guid ActivationCodeId { get; set; }
        public string MedicoCode { get; set; }
        public string ActivactionCode { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public Byte[] Image { get; set; }
    }
}
