using System;

namespace Me_dico.it.Model
{
    public class AnswerComment : BaseDTO
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Description { get; set; }
        public virtual Answer AnswerSource { get; set; }
    }
}