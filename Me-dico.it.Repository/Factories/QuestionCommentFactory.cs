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
    public class QuestionCommentFactory : IFactory<QuestionComment, Model.QuestionComment>
    {
        readonly IMedicoRepository _repository;
        public QuestionCommentFactory()
        {
            _repository = new MedicoRepository(new MedicoContext());
        }
        public QuestionComment CreateEntity(Model.QuestionComment comment)
        {
            return new QuestionComment
            {
                Id = comment.Id,
                Description = comment.Description,
                UpdateDate = comment.UpdateDate,
                UserId = comment.UserId
            };
        }
        public Model.QuestionComment CreateDto(QuestionComment comment)
        {
            var tmpUser = _repository.GetUserById(comment.UserId);

            return new Model.QuestionComment
            {
                Id = comment.Id,
                Description = comment.Description,
                UpdateDate = comment.UpdateDate,
                UserId = comment.UserId,
                QuestionSource = null, // todo
                UserName = tmpUser.NickName
            };
        }

        public object CreateDataShapedObject(Entities.QuestionComment qc, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return qc;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = qc.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(qc, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }

        public object CreateDataShapedObject(Model.QuestionComment qc, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return qc;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = qc.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(qc, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}
