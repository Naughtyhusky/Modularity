using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Commands
{
    public class RoleMenuBingingCmd(long menuId) : CommandBase()
    {
        public long MenuId { get; set; } = menuId;
    }
}
