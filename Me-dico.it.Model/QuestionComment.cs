using System;

namespace Me_dico.it.Model
{
    public class QuestionComment : BaseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public Question QuestionSource { get; set; }
    }
}