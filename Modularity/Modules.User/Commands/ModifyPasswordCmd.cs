using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Commands
{
    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <param name="newPassword"></param>
    public class ModifyPasswordCmd(string password, string newPassword) : CommandBase()
    {       
        /// <summary>
        /// 旧密码
        /// </summary>
        public string Password { get; set; } = password;

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; } = newPassword;
    }
}
