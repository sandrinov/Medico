using Me_dico.it.WebClient.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Me_dico.it.Models
{
    public class MyAnswerViewModel
    {
        public IPagedList<Model.Answer> MyAnswers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}