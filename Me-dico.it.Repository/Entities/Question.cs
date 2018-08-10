using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("Question")]
    public class Question :BaseEntity
    {
        public Question()
        {
            QuestionComments = new HashSet<QuestionComment>();
            Answers = new HashSet<Answer>();
            Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }
        public virtual User User { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public int VoteCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }

    }
}
