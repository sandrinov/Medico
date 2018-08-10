using System;
using System.Collections.Generic;

namespace Me_dico.it.Model
{
    public class Answer : BaseDTO
    {
        public int Id { get; set; }
        //public virtual User User { get; set; }
        public string UserName { get; set; }
        public virtual Question QuestionSource { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<AnswerComment> AnswerComments { get; set; }
        public int VoteCount { get; set; }
    }
}