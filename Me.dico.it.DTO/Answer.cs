using System;
using System.Collections.Generic;

namespace Me.dico.it.DTO
{
    public class Answer : BaseDTO
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Question QuestionSource { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<AnswerComment> AnswerComments { get; set; }
        public int VoteCount { get; set; }
    }
}