using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Events
{
    public class RoleNameChangedEvent(long roleId, string roleName) : EventBase()
    {
        public long RoleId { get; set; } = roleId;

        public string RoleName { get; set; } = roleName;
    }
}
