﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Entities
{
    [Table("Answer")]
    public class Answer : BaseEntity
    {
        public Answer()
        {
            AnswerComments = new HashSet<AnswerComment>();
        }
        public int Id { get; set; }
        //public virtual User User { get; set; }
        public string UserName { get; set; }
        public virtual Question QuestionSource { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
        public int VoteCount { get; set; }

    }
}
