using Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Resource.Entities
{
    public class Menu : EntityBase
    {
        public Menu(string name, int flag, int parent, int order, string route, string description, long createUserId, string createUserName):base(createUserId, createUserName)
        {
            Name = name;
            Flag = flag;
            Parent = parent;
            Order = order;
            Route = route;
            Description = description;
        }

        protected Menu() { }
      
        public string Name { get; private set; }

        public int Flag { get; private set; }

        public int Parent { get; private set; }

        public int Order { get; private set; }

        public string Route { get; private set; }

        public string Description { get; private set; }
    }
}
