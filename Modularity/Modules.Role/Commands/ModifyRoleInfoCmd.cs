using Infrastructure.Bus;
using Modules.Role.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Commands
{
    public class ModifyRoleInfoCmd(long roleId, string name, RoleType type, string description) : CommandBase()
    {
        public long RoleId { get; set; } = roleId;

        public string Name { get; set; } = name;

        public string Description { get; set; } = description;

        public RoleType Type { get; set; } = type;
    }
}
