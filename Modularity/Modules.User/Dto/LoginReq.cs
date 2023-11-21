using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Dto
{
    public class LoginReq
    {
        public  string Account { get; set; }

        public string Password { get; set; }
    }
}
