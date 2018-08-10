using Me_dico.it.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me_dico.it.Models
{
    public class MenuViewModel
    {
        public IEnumerable<MenuVoice> MenuVoices { get; set; }
        public double Reputation { get; set; }
        public double Health { get; set; }
        public string UserName { get; set; }
        public byte[] Avatar { get; set; }

    }
}