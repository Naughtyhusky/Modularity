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
    /// 修改用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <param name="gender"></param>
    public class ModifyUserCmd(long userId, string name, Gender gender) : CommandBase()
    {

        /// <summary>
        /// id
        /// </summary>
        public long UserId { get; set; } = userId;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; } = gender;
    }
}
