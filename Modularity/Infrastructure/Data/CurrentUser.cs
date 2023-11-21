using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CurrentUser
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? TraceIdentifier { get; set; }
    }
}
