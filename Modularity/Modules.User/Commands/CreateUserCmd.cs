using Infrastructure.Bus;
using Modules.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Commands
{
    /// <summary>
    /// 创建用户
    /// </summary>
    public class CreateUserCmd(string account, string password, string name, Gender gender, long roleId) : CommandBase()
    {

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get;  set; } = account;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get;  set; } = password;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get;  set; } = name;

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get;  set; } = gender;

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get;  set; }= roleId;
    }
}
