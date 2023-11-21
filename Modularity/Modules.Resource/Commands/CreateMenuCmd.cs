using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Resource.Commands
{
    public class CreateMenuCmd(string name, int flag, int parent, int order, string route, string description) : CommandBase()
    {
        public string Name { get; set; } = name;

        public int Flag { get; set; } = flag;

        public int Parent { get; set; } = parent;

        public int Order { get; set; } = order;

        public string Route { get; set; } = route;

        public string Description { get; set; } = description;
    }
}
