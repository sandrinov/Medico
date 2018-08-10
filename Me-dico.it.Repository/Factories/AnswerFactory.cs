using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Factories
{
    public class AnswerFactory : IFactory<Answer, Model.Answer>
    {
        readonly IFactory<AnswerComment, Model.AnswerComment> _aCommentFactory;
        readonly IFactory<Question, Model.Question> _qFactory;

        public AnswerFactory(
            IFactory<AnswerComment, Model.AnswerComment> answerCommentFactory      
            )
        {
            _aCommentFactory = answerCommentFactory;
        }
        public Answer CreateEntity(Model.Answer answer)
        {
            return new Answer
            {
                Id = answer.Id,
                Description = answer.Description,
                UpdateDate = answer.UpdateDate,
                UserId = answer.UserId,
                VoteCount = answer.VoteCount,
                AnswerComments = answer.AnswerComments.Select(e => _aCommentFactory.CreateEntity(e)).ToList(),
                QuestionSource = new Question()
                {
                    Id = answer.QuestionSource.Id,
                    Title = answer.QuestionSource.Title,
                    Description = answer.QuestionSource.Description,
                    UserName = answer.QuestionSource.UserName,
                    UserId = answer.QuestionSource.UserId,
                },
                UserName = answer.UserName
            };
        }
        public Model.Answer CreateDto(Answer answer)
        {
            return new Model.Answer
            {
                Id = answer.Id,
                Description = answer.Description,
                UpdateDate = answer.UpdateDate,
                UserId = answer.UserId,
                VoteCount = answer.VoteCount,
                AnswerComments = answer.AnswerComments.Select(e => _aCommentFactory.CreateDto(e)).ToList(),
                QuestionSource = new Model.Question() { Id = answer.QuestionSource.Id,
                                                        Title = answer.QuestionSource.Title,
                                                        Description = answer.QuestionSource.Description,
                                                        UserName = answer.QuestionSource.UserName,
                                                        UserId = answer.QuestionSource.UserId,
                                                        PostedTime = answer.QuestionSource.UpdateDate.HasValue?answer.QuestionSource.UpdateDate.Value : DateTime.Now
                },
                UserName = answer.UserName 
            };
        }
        public object CreateDataShapedObject(Answer answer, List<string> lstOfFields)
        {

            return CreateDataShapedObject(CreateDto(answer), lstOfFields);
        }


        public object CreateDataShapedObject(Model.Answer answer, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return answer;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = answer.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(answer, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }


    }
}
