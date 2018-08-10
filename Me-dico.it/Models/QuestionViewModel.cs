using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Me_dico.it.WebClient.Helpers;
using PagedList;


namespace Me_dico.it.Models
{
    public class QuestionViewModel
    {
        public IPagedList<Model.Question> Questions { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}