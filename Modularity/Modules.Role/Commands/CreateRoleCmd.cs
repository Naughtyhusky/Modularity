using Infrastructure.Bus;
using Modules.Role.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Commands
{
    public class CreateRoleCmd(string name, RoleType type, string description, IEnumerable<long> menuIds) : CommandBase()
    {
        public string Name { get; set; } = name;

        public RoleType Type { get; set; } = type;

        public string Description { get; set; } = description;

        public IEnumerable<long> MenuIds { get; set; } = menuIds;
    }
}
