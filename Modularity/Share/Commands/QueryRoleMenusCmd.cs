using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Commands
{
    public class QueryRoleMenusCmd(long roleId) : CommandBase()
    {
        public long RoleId { get; set; } = roleId;
    }
}
