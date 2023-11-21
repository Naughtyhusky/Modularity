using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Org.Commands
{
    public class CreateCompanyCmd(string name, string description) : CommandBase()
    {
        public string Name { get; set; } = name;

        public string Description { get; set; } = description;
    }
}
