using Me_dico.it.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoContext : DbContext
    {
        public MedicoContext()
            : base("name=MedicoContext")
        {

        }

        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<QuestionComment> QuestionComments { get; set; }
        public virtual DbSet<AnswerComment> AnswerComments { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ActivationCode> ActivationCodes { get; set; }
        public virtual DbSet<MenuVoice> MenusVoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            #region Question Configuration
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.QuestionSource)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionComments)
                .WithRequired(e => e.QuestionSource)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Questions);

            #endregion

            #region Answer Configuration
            modelBuilder.Entity<Answer>()
                .HasMany(e => e.AnswerComments)
                .WithRequired(e => e.AnswerSource)
                .WillCascadeOnDelete(true);
            #endregion

            #region User Configuration
            modelBuilder.Entity<User>()
                .Property(x => x.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>()
                .HasOptional(e => e.ProfileUser)
                .WithRequired(e => e.User);
            #endregion

        //    #region ActivactionCode
        //    modelBuilder.Entity<ActivationCode>()
        //          .Property(x => x.ActivationCodeId)
        //          .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //    #endregion
        }
    }
}
