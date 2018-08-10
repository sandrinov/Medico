using Me_dico.it.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository
    {
        public RepositoryActionResult<Answer> AddAnswer(Answer a)
        {
            #region try
            try
            {
                _ctx.Answers.Add(a);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Answer>(a, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Answer>(a, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Answer>(null, RepositoryActionStatus.Error, ex);
            }
            #endregion
        }

        public RepositoryActionResult<AnswerComment> AddAnswerComment(AnswerComment ac)
        {
            #region try
            try
            {
                _ctx.AnswerComments.Add(ac);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<AnswerComment>(ac, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<AnswerComment>(ac, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<AnswerComment>(null, RepositoryActionStatus.Error, ex);
            }
            #endregion
        }

        public Answer GetAnswer(int id)
        {
            return _ctx.Answers
                 .FirstOrDefault(a => a.Id == id);
        }

        public RepositoryActionResult<Answer> UpdateAnswer(Answer a)
        {
            try
            {

                // you can only update when an question already exists for this id

                var existingAnswer = _ctx.Answers.FirstOrDefault(exg => exg.Id == a.Id);

                if (existingAnswer == null)
                {
                    return new RepositoryActionResult<Answer>(a, RepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingAnswer).State = EntityState.Detached;

                // attach & save
                _ctx.Answers.Attach(a);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(a).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Answer>(a, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Answer>(a, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Answer>(null, RepositoryActionStatus.Error, ex);
            }
        }

        #region async 
        public async Task<IQueryable<Answer>> MyAnswers(Guid userId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Answers
                        .Include(a => a.QuestionSource)
                        .OrderByDescending(a => a.UpdateDate)
                        .Where(a => a.UserId == userId);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        #endregion
    }
}
