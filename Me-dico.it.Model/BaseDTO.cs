using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Model
{
    public class BaseDTO
    {
        public Guid UserId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
