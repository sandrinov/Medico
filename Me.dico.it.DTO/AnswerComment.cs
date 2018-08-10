using System;

namespace Me.dico.it.DTO
{
    public class AnswerComment : BaseDTO
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Description { get; set; }
        public virtual Answer AnswerSource { get; set; }
    }
}