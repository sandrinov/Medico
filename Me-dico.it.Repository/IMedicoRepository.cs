using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.LocalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository
{
    public interface IMedicoRepository
    {

        #region Answers
        RepositoryActionResult<Answer> AddAnswer(Answer a);
        RepositoryActionResult<AnswerComment> AddAnswerComment(AnswerComment ac);
        RepositoryActionResult<Answer> UpdateAnswer(Answer a);
        Task<IQueryable<Answer>> MyAnswers(Guid userId);
        Answer GetAnswer(int id);

        #endregion

        #region Questions
        Question GetQuestion(int id);
        IQueryable<Question> GetQuestions(Guid? userId);
        RepositoryActionResult<Question> UpdateQuestion(Question q);
        RepositoryActionResult<Question> AddQuestion(Question q);
        RepositoryActionResult<QuestionComment> AddQuestionComment(QuestionComment qc);
        RepositoryActionResult<List<TitleViewModel>> GetTitles(String searchTerm);


        Task<IQueryable<Question>> GetQuestionsByText(String likeText);
        Task<IQueryable<Question>> GetQuestionsByTag(int tagId);
        Task<Question> GetQuestionAsync(int id);
        Task<IQueryable<Question>> GetQuestionsAsync(Guid? userId);
        Task<IQueryable<Question>> GetQuestionsWithoutIncludeAsync(Guid? userId);
        Task<IQueryable<Question>> GetQuestionsLike(Guid? userId, String textLike);
        #endregion

        #region Users
        User GetUserByUserName(string userName);      
        User GetUserById(Guid userId);
        User AddUser(Model.User user);


        Task<User> AddUserAsync(Model.User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<IQueryable<User>> GetUsersAsync(Guid? userId);
        Task<IQueryable<User>> GetUsersWithoutIncludeAsync(Guid? id);
        Task<Profile> GetProfileAsync(int id);
        Task<Profile> GetProfileByGuid(string Guid);

        #endregion

        #region tags
        Task<IQueryable<Tag>> GetTagsAsync();
        Task<Tag> GetTagByIdAsync(int tagId);
        RepositoryActionResult<Tag> AddTag(Tag t);
        #endregion

        RepositoryActionResult<List<StatisticsDataViewModel>> GetStatistics(String userName);


        #region Menu Voices
        Task<IQueryable<MenuVoice>> GetMenuVoicesAsync();
        #endregion

        #region Authentication
        Task<ActivationCode> TryRegisterAsync(ActivationCode act);
        Task<bool> CanRegister(string ActivactionCode);
        
        #endregion
    }
}
