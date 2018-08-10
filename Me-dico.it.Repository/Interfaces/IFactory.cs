
using Me_dico.it.Repository.Entities;
using System.Collections.Generic;

namespace Me_dico.it.Repository.Interfaces
{
    public interface IFactory<Entity, DTO>
    {
        Entity CreateEntity(DTO dto);
        DTO CreateDto(Entity entity);
        object CreateDataShapedObject(Entity entity, List<string> lstOfFields);
        object CreateDataShapedObject(DTO dto, List<string> lstOfFields);
    }
}