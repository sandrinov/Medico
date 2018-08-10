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
    public class UserFactory : IFactory<User, Model.User>
    {


        public User CreateEntity(Model.User user)
        {
            return new User
            {
                UserId = user.UserId,
                NickName = user.NickName,
                Email = user.Email,
                Password = user.Password,
                ActivationCode = user.ActivationCode,
                IdMedicalCard = user.IdMedicalCard,
                CreateDate = user.CreateDate,
                ActivationDate = user.ActivationDate.HasValue ? user.ActivationDate.Value : DateTime.Now,
                LastLoginDate = user.LastLoginDate.HasValue ? user.LastLoginDate.Value : DateTime.Now,
                Status = user.Status,
                Role = user.Role,
                ProfileUser = new Profile() { Id = user.ProfileUser.Id }

            };
        }
        public Model.User CreateDto(User user)
        {
            return new Model.User
            {
                UserId = user.UserId,
                NickName = user.NickName,
                Email = user.Email,
                Password = user.Password,
                ActivationCode = user.ActivationCode,
                IdMedicalCard = user.IdMedicalCard,
                CreateDate = user.CreateDate.HasValue ? user.CreateDate.Value : DateTime.Now,
                ActivationDate = user.ActivationDate.HasValue ? user.ActivationDate.Value : DateTime.Now,
                LastLoginDate = user.LastLoginDate.HasValue ? user.LastLoginDate.Value : DateTime.Now,
                Status = user.Status,
                Role = user.Role,
                ProfileUser = new Model.Profile() { Id = user.ProfileUser.Id }
            };
        }
        public object CreateDataShapedObject(User user, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return user;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = user.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(user, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
        public object CreateDataShapedObject(Model.User user, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return user;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = user.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(user, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }


    }
}
