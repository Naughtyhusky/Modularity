using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Dto
{
    public class MenuItem
    {
        public long Id { get; set; }

        public string? Name { get;  set; }

        public int Flag { get;  set; }

        public int Parent { get;  set; }

        public int Order { get;  set; }

        public string? Route { get;  set; }

        public string? Description { get;  set; }
    }
}
