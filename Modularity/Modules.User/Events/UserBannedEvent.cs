using Infrastructure.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Events
{
    /// <summary>
    /// 用户账号被禁用事件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="account"></param>
    /// <param name="userName"></param>
    public class UserBannedEvent(long id, string account, string userName) : EventBase()
    {
        public long Id { get; set; } = id;

        public string Account { get; set; } = account;
        public string UserName { get; set; } = userName;

    }
}
