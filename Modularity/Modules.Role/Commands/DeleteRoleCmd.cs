using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Commands
{
    public class DeleteRoleCmd(long id) : CommandBase()
    {
        public long Id { get; set; } = id;
    }
}
