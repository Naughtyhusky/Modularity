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
    /// 修改用户状态
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="state"></param>
    public class ModifyStateCmd(long userId,State state) : CommandBase()
    {
        /// <summary>
        /// id
        /// </summary>
        public long UserId { get; set; } = userId;

        /// <summary>
        /// 状态
        /// </summary>
        public State State { get; set; } = state;
    }
}
