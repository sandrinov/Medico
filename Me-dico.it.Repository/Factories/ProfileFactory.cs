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
    public class ProfileFactory : IFactory<Profile, Model.Profile>
    {
        readonly IFactory<User, Model.User> _uFactory;

        public ProfileFactory(IFactory<User, Model.User>  uFactory)
        {
            _uFactory = uFactory;
        }

        public Profile CreateEntity(Model.Profile profile)
        {
            return new Profile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Title = profile.Title,
                Photo = profile.Photo,
                Address1 = profile.Address1,
                Address2 = profile.Address1,
                City = profile.City,
                Country = profile.Country,
                Mobile = profile.Mobile,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                User = _uFactory.CreateEntity(profile.User)

            };
        }
        public Model.Profile CreateDto(Profile profile)
        {
            return new Model.Profile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Title = profile.Title,
                Photo = profile.Photo,
                Address1 = profile.Address1,
                Address2 = profile.Address1,
                City = profile.City,
                Country = profile.Country,
                Mobile = profile.Mobile,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                User = _uFactory.CreateDto(profile.User)
            };
        }
        public object CreateDataShapedObject(Profile profile, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return profile;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = profile.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(profile, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
        public object CreateDataShapedObject(Model.Profile profile, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return profile;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = profile.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(profile, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }


    }
}
