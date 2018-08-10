using System;
using System.Collections.Generic;
using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using System.Linq;
using System.Dynamic;
using System.Reflection;

namespace Me_dico.it.Repository.Factories
{
    public class AnswerCommentFactory : IFactory<AnswerComment, Model.AnswerComment>
    {
        readonly IMedicoRepository _repository;
        readonly IFactory<User, Model.User> _userFactory;
        public AnswerCommentFactory(IFactory<User, Model.User> userFactory)
        {
            _userFactory = userFactory;
            _repository = new MedicoRepository(new MedicoContext());
        }

        public AnswerComment CreateEntity(Model.AnswerComment comment)
        {
            return new AnswerComment
            {
                Id = comment.Id,
                Description = comment.Description,
                UpdateDate = comment.UpdateDate,
                UserId = comment.UserId,
                NickName = comment.NickName
            };
        }
        public Model.AnswerComment CreateDto(AnswerComment comment)
        {
            var tmpUser = _repository.GetUserById(comment.UserId);

            return new Model.AnswerComment
            {
                Id = comment.Id,
                Description = comment.Description,
                UpdateDate = comment.UpdateDate,
                UserId = comment.UserId,
                NickName = comment.NickName,
                AnswerSource = null, // todo
            };
        }

        public object CreateDataShapedObject(Entities.AnswerComment ac, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return ac;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = ac.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(ac, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }

        public object CreateDataShapedObject(Model.AnswerComment ac, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return ac;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = ac.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(ac, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}