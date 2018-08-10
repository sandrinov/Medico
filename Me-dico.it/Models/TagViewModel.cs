using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me_dico.it.Models
{
    public class TagViewModel
    {
        public TagViewModel()
        {
            int x = 0;
        }
        public IEnumerable<Model.Tag> Tags { get; set; }
    }
}