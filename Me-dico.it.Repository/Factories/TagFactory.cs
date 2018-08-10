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
    public class TagFactory : IFactory<Tag, Model.Tag>
    {
        public Tag CreateEntity(Model.Tag tag)
        {
            return new Tag
            {
                 Id = tag.Id,
                 Description = tag.Description,
            };
        }
        public Model.Tag CreateDto(Tag tag)
        {
            return new Model.Tag
            {
                Id = tag.Id,
                Description = tag.Description
            };
        }
        public object CreateDataShapedObject(Entities.Tag tag, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return tag;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = tag.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(tag, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
        public object CreateDataShapedObject(Model.Tag tag, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
            {
                return tag;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = tag.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(tag, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}
