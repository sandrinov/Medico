using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.Repository.Factories
{
    public class MenuFactory : IFactory<MenuVoice, Model.MenuVoice>
    {
        public MenuVoice CreateEntity(Model.MenuVoice menu)
        {
            return new MenuVoice
            {
                Id = menu.Id,
                Text = menu.Text,
                IconName = menu.IconName,
                Action = menu.Action,
                Controller = menu.Controller,
                Role = menu.Role,
                UserStatus = menu.UserStatus
            };
        }
        public Model.MenuVoice CreateDto(MenuVoice menu)
        {
            return new Model.MenuVoice
            {
                Id = menu.Id,
                Text = menu.Text,
                IconName = menu.IconName,
                Action = menu.Action,
                Controller = menu.Controller,
                Role = menu.Role,
                UserStatus = menu.UserStatus
            };
        }

        public object CreateDataShapedObject(Entities.MenuVoice entity, List<string> lstOfFields)
        {
            throw new NotImplementedException();
        }

        public object CreateDataShapedObject(Model.MenuVoice dto, List<string> lstOfFields)
        {
            throw new NotImplementedException();
        }
    }
}
