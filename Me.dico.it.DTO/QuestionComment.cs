using System;

namespace Me.dico.it.DTO
{
    public class QuestionComment : BaseDTO
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Description { get; set; }
        public Question QuestionSource { get; set; }
    }
}