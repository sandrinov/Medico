using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.LocalModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository
    {
        public Question GetQuestion(int id)
        {
            Question existingQuestion = _ctx.Questions.FirstOrDefault(e => e.Id == id);
            existingQuestion.ViewsCount++;

            try
            {
                var result = _ctx.SaveChanges();
                if (result > 0)
                {

                    return _ctx.Questions
                      .Include(e => e.Answers)
                      .Include(e => e.Tags)
                      .Include(e => e.QuestionComments)
                      .FirstOrDefault(e => e.Id == id);
                }
                else
                    return new Question() { Description = "Aggiornamento non andato a buon fine" };
            }
            catch (Exception e)
            {
                return new Question() { Description = e.InnerException.Message };
            }
        }
        public IQueryable<Question> GetQuestions(Guid? userId)
        {

            return _ctx.Questions
                .Include(e => e.Tags)
                .OrderByDescending(q => q.UpdateDate)
                .Where(e => (userId == null || e.UserId == userId));
        }
        public RepositoryActionResult<Question> UpdateQuestion(Question q)
        {
            try
            {

                // you can only update when an question already exists for this id

                var existingQuestion = _ctx.Questions.FirstOrDefault(exg => exg.Id == q.Id);

                if (existingQuestion == null)
                {
                    return new RepositoryActionResult<Question>(q, RepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingQuestion).State = EntityState.Detached;

                // attach & save
                _ctx.Questions.Attach(q);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(q).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Question>(q, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Question>(q, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Question>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<Question> AddQuestion(Question q)
        {

            #region try
            try
            {
                _ctx.Questions.Add(q);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Question>(q, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Question>(q, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Question>(null, RepositoryActionStatus.Error, ex);
            }
            #endregion
        }
        public RepositoryActionResult<QuestionComment> AddQuestionComment(QuestionComment qc)
        {
            #region try
            try
            {
                _ctx.QuestionComments.Add(qc);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<QuestionComment>(qc, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<QuestionComment>(qc, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<QuestionComment>(null, RepositoryActionStatus.Error, ex);
            }
            #endregion
        }
        public RepositoryActionResult<List<TitleViewModel>> GetTitles(String searchTerm)
        {
            #region try
            try
            {
                var result = from q in _ctx.Questions
                             where q.Title.Contains(searchTerm)
                             select q.Title;
                List<String> resultStringList = result.ToList();
                List<TitleViewModel> titles = new List<TitleViewModel>();
                foreach (var item in resultStringList)
                {
                    titles.Add(new TitleViewModel() { Value=item });
                }
                if(titles.Count > 0)
                    return new RepositoryActionResult<List<TitleViewModel>>(titles, RepositoryActionStatus.Ok);
                else
                    return new RepositoryActionResult<List<TitleViewModel>>(titles, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<List<TitleViewModel>>(null, RepositoryActionStatus.Error, ex);
            }
            #endregion
        }
      
        #region Async Operations
        public async Task<Question> GetQuestionAsync(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Question existingQuestion = _ctx.Questions.FirstOrDefault(e => e.Id == id);
                    existingQuestion.ViewsCount++;

                    var result = _ctx.SaveChanges();


                    if (result > 0)
                    {


                        return _ctx.Questions
                                                  .Include(e => e.Answers)
                                                  .Include(e => e.Answers.Select(a => a.AnswerComments))
                                                  //.Include(e => e.Answers.Select(a => a.AnswerComments.Select(ac => ac.User)))
                                                  .Include(e => e.Tags)
                                                  .Include(e => e.QuestionComments)
                                                  .FirstOrDefault(e => e.Id == id);
                    }
                    else
                        return new Question() { Description = "Aggiornamento non andato a buon fine" };
                }
                catch (Exception e)
                {
                    string err = e.Message;
                    return new Question() { Description =  e.InnerException.Message };
                }
            });
        }
        public async Task<IQueryable<Question>> GetQuestionsWithoutIncludeAsync(Guid? userId)
        {

            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Questions
                        .OrderByDescending(q=>q.UpdateDate)
                        .Where(e => (userId == null || e.UserId == userId));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        public async Task<IQueryable<Question>> GetQuestionsByTag(int tagId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var query = from question in _ctx.Questions
                                where question.Tags.Any(t=>t.Id == tagId)
                                select question;

                    return query;



                    //Tag tag = _ctx.Tags.FirstOrDefault(t => t.Id == tagId);
                    //if (tag != null)
                    //{

                    //    return _ctx.Questions
                    //        .OrderByDescending(q => q.UpdateDate)
                    //        .Where(e => e.Tags.Contains(tag));
                    //}
                    //else
                    //    return null;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        public async Task<IQueryable<Question>> GetQuestionsLike(Guid? userId, String textLike)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _ctx.Questions
                        .OrderByDescending(q=>q.UpdateDate)
                        .Where(e => (userId == null || e.UserId == userId))
                        .Where(e => e.Title.Contains(textLike));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        public async Task<IQueryable<Question>> GetQuestionsAsync(Guid? userId)
        {

            return await Task.Run(() =>
            {
                return _ctx.Questions
                    .Include(e => e.Tags)
                    .OrderByDescending(q => q.UpdateDate)
                    .Include(e => e.QuestionComments)
                    .Where(e => (userId == null || e.UserId == userId));
            });
        }
        public async Task<IQueryable<Question>> GetQuestionsByText(String likeText)
        {
            return await Task.Run(() =>
            {

                return _ctx.Questions
                .OrderByDescending(q => q.UpdateDate)
                .Where(q => q.Description.Contains(likeText));
            });
        }
        #endregion
    }
}
