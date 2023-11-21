using Infrastructure.Data;
using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Dto
{
    public class AuthInfo(UserDto user, IEnumerable<TreeModel<MenuItem>> menus)
    {
       
        public UserDto User { get; set; } = user;

        public IEnumerable<TreeModel<MenuItem>> Menus { get; set; } = menus;
    }
}
