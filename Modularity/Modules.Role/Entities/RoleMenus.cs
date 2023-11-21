using Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Entities
{
    public class RoleMenus : EntityBase
    {
        public RoleMenus(long roleId, long menuId, long createUserId, string createUserName) : base(createUserId, createUserName)
        {
            RoleId = roleId;
            MenuId = menuId;
        }

        protected RoleMenus() { }

        public long RoleId { get; private set; }

        public long MenuId { get; private set; }

        public virtual Role? Role { get; set; }



        public static IEnumerable<RoleMenus> GenerateRoleMenus(long roleId, IEnumerable<long> MenuIds, long createUserId, string createUserName)
        {
            foreach (var menuId in MenuIds)
            {
                yield return new RoleMenus(roleId, menuId, createUserId, createUserName);
            }
        }
    }
}
