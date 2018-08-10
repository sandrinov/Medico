using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Me_dico.it.Model
{
    public class Question : BaseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<QuestionComment> QuestionComments { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public int VoteCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
