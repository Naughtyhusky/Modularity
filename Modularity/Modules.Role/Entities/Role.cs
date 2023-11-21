using Infrastructure.DataBase;
using Modules.Role.Enums;
using Share.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Entities
{
    public class Role : EntityBase
    {
        public Role(string name, string? description, RoleType type, long createUserId, string createUserName) : base(createUserId, createUserName)
        {
            Name = name;
            Description = description ?? string.Empty;
            Type = type;
        }

        public Role(string name, RoleType type, string description, IEnumerable<long> roleMenuIds, long createUserId, string createUserName) : base(createUserId, createUserName)
        {
            Name = name;
            Type = type;
            Description = description ?? string.Empty;
            RoleMenus = Entities.RoleMenus.GenerateRoleMenus(Id, roleMenuIds, createUserId, createUserName).ToList();
        }

        protected Role() { }

        public string Name { get; private set; }

        public RoleType Type { get; private set; }

        public string Description { get; private set; }

        public virtual List<RoleMenus> RoleMenus { get; set; }

        public void Modify(string name, RoleType type, string description, long updateUserId, string updateUserName)
        {
            if (Name != null || Type != type || Description != description)
            {
                if (Name != name)
                {
                    var @event = new RoleNameChangedEvent(Id, name);

                    AddDomainEvent(@event);
                }

                Name = name;
                Type = type;
                Description = description;

                UpdateRecord(updateUserId, updateUserName);

            }
        }
    }
}
