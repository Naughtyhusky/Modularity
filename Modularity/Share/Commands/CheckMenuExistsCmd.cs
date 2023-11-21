using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Commands
{
    public class CheckMenuExistsCmd(IEnumerable<long> menuIds) : CommandBase()
    {
        public IEnumerable<long> MenuIds { get; set; } = menuIds;
    }
}
