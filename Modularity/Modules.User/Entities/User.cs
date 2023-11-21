using Infrastructure.DataBase;
using Modules.User.Enums;
using Modules.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : EntityBase
    {
        public User(string account, string password, string name, Gender gender, long roleId, string roleName, long createUserId, string createUserName) : base(createUserId, createUserName)
        {
            Account = account;
            Name = name;
            Gender = gender;
            State = State.Normal;
            RoleId = roleId;
            RoleName = roleName;

            GeneratePassword(password);
        }

        /// <summary>
        /// 
        /// </summary>
        protected User() { }



        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string Salt { get; private set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; private set; }

        /// <summary>
        /// 状态
        /// </summary>
        public State State { get; private set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; private set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; private set; }


        /// <summary>
        /// 随机生成盐
        /// </summary>
        private void GenerateSalt()
        {
            Salt = Guid.NewGuid().ToString("N").ToLower();
        }

        /// <summary>
        /// 密码混淆
        /// </summary>
        /// <param name="userPassword">用户密码的32位MD5加密的密文</param>
        private void GeneratePassword(string userPassword)
        {
            GenerateSalt();

            Password = MixPassword(userPassword);
        }

        /// <summary>
        /// 混淆密码，得到加盐密文
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        private string MixPassword(string userPassword)
        {
            var front = Salt[0..10];

            var backend = Salt[10..];

            return (front + userPassword.ToLower() + backend).Md5().ToLower();
        }

        /// <summary>
        /// 校验密码是否正确
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string userPassword)
        {
            var mixPassword = MixPassword(userPassword);

            return Password == mixPassword;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword"></param>
        public void ModifyPassword(string newPassword)
        {
            GeneratePassword(newPassword);

            UpdateRecord(Id, Name);
        }

        /// <summary>
        /// 账号是否是可使用的
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            return State == State.Normal;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="gender">性别</param>
        public void ModifyUserInfo(string name, Gender gender)
        {
            if (Name != name || Gender != gender)
            {
                Name = name;
                Gender = gender;
                UpdateRecord(Id, Name);
            }
        }

        /// <summary>
        /// 修改账号状态
        /// </summary>
        /// <param name="state">账号状态</param>
        /// <param name="updateUserId">修改人id</param>
        /// <param name="updateUserName">修改人名称</param>
        public void ChangeState(State state, long updateUserId, string updateUserName)
        {
            if (State != state)
            {
                if (state== State.Disable)
                {
                    UserBannedEvent @event = new(Id, Account, Name);

                    AddDomainEvent(@event);
                }

                State = state;

                UpdateRecord(updateUserId, updateUserName);
            }
        }
    }
}
