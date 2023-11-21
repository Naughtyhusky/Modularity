using Modules.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Dto
{
    public class UserDto
    {

        public long Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string? Account { get;  set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? Name { get;  set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get;  set; }

        /// <summary>
        /// 状态
        /// </summary>
        public State State { get;  set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get;  set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string? RoleName { get;  set; }
    }
}
