using Me_dico.it.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Me_dico.it.Repository.Helpers;
using Me_dico.it.Repository.Interfaces;

namespace Me_dico.it.Repository.Factories
{
    public class QuestionFactory : IFactory<Question, Model.Question> 
    {

        readonly IFactory<Answer, Model.Answer> _ansFactory;
        readonly IFactory<Tag, Model.Tag> _tgFactory;
        readonly IFactory<QuestionComment, Model.QuestionComment> _queCommentFactory;
        public QuestionFactory(  IFactory<Answer, Model.Answer> ansFactory,
                                 IFactory<Tag, Model.Tag> tagFactory,
                                 IFactory<QuestionComment, Model.QuestionComment> queCommentFactory)
        {
            _ansFactory = ansFactory;
            _tgFactory = tagFactory;
            _queCommentFactory = queCommentFactory;
        }
        public Question CreateEntity(Model.Question question)
        {
            return new Question
            {
                Id = question.Id,
                Title = question.Title,
                Description = question.Description,
                UserId = question.UserId,
                VoteCount = question.VoteCount,
                ViewsCount = question.ViewsCount,
                AnswersCount = question.AnswersCount,
                UpdateDate = question.UpdateDate,
                QuestionComments = question.QuestionComments == null ? new List<QuestionComment>() : question.QuestionComments.Select(e => _queCommentFactory.CreateEntity(e)).ToList(),
                Answers = question.Answers == null ? new List<Answer>() : question.Answers.Select(e => _ansFactory.CreateEntity(e)).ToList(),
                Tags = question.Tags == null ? new List<Tag>() : question.Tags.Select(e => _tgFactory.CreateEntity(e)).ToList()
              
            };
        }

        public Model.Question CreateDto(Question question)
        {
            return new Model.Question
            {
                Id = question.Id,
                Title = question.Title,
                Description = question.Description,
                UserId = question.UserId,
                VoteCount = question.VoteCount,
                ViewsCount = question.ViewsCount,
                AnswersCount = question.AnswersCount,
                UpdateDate = question.UpdateDate,
                QuestionComments = question.QuestionComments.Select(e => _queCommentFactory.CreateDto(e)).ToList(),
                Answers = question.Answers.Select(e => _ansFactory.CreateDto(e)).ToList(),
                Tags = question.Tags.Select(e => _tgFactory.CreateDto(e)).ToList(),
                UserName = question.UserName 
            };
        }

        public object CreateDataShapedObject(Question question, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreateDto(question), lstOfFields);
        }

        public object CreateDataShapedObject(Model.Question question, List<string> lstOfFields)
        {
            // work with a new instance, as we'll manipulate this list in this method
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            if (!lstOfFieldsToWorkWith.Any())
            {
                return question;
            }
            else
            {

                // does it include any answer-related field?
                var lstOfAnswerFields = lstOfFieldsToWorkWith.Where(f => f.Contains("answers")).ToList();

                // if one of those fields is "answers", we need to ensure the FULL answer is returned.  If
                // it's only subfields, only those subfields have to be returned.

                bool returnPartialAnswer = lstOfAnswerFields.Any() && !lstOfAnswerFields.Contains("answers");

                // if we don't want to return the full answer, we need to know which fields
                if (returnPartialAnswer)
                {
                    // remove all expense-related fields from the list of fields,
                    // as we will use the CreateDateShapedObject function in AnswerFactory
                    // for that.

                    lstOfFieldsToWorkWith.RemoveRange(lstOfAnswerFields);
                    lstOfAnswerFields = lstOfAnswerFields.Select(f => f.Substring(f.IndexOf(".") + 1)).ToList();

                }
                else
                {
                    // we shouldn't return a partial answers, but the consumer might still have
                    // asked for a subfield together with the main field, ie: answer, answer.id.  We 
                    // need to remove those subfields in that case.

                    lstOfAnswerFields.Remove("answers");
                    lstOfFieldsToWorkWith.RemoveRange(lstOfAnswerFields);
                }

                // create a new ExpandoObject & dynamically create the properties for this object

                // if we have an answer

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFieldsToWorkWith)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = question.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(question, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                if (returnPartialAnswer)
                {
                    // add a list of answers, and in that, add all those answers
                    List<object> answers = new List<object>();
                    foreach (var answer in question.Answers)
                    {
                        answers.Add(_ansFactory.CreateDataShapedObject(answer, lstOfAnswerFields));
                    }

                    ((IDictionary<String, Object>)objectToReturn).Add("answers", answers);
                }


                return objectToReturn;
            }
        }
    }
}
