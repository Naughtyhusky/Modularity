using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Migrations
{
    [Migration(202311191800, "User模块初始化")]
    public class Init : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
           Create.Table("t_user").WithDescription("用户信息表")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("id")
                 .WithColumn("account").AsString(32).NotNullable().Unique().WithColumnDescription("登录账号")
                 .WithColumn("password").AsString(64).NotNullable().WithColumnDescription("密码密文")
                 .WithColumn("salt").AsString(64).NotNullable().WithColumnDescription("盐")
                 .WithColumn("name").AsString(64).NotNullable().WithColumnDescription("用户名")
                 .WithColumn("gender").AsInt32().NotNullable().WithColumnDescription("性别")
                 .WithColumn("state").AsInt32().NotNullable().WithColumnDescription("账号状态")
                 .WithColumn("role_id").AsInt64().NotNullable().WithColumnDescription("角色id")
                 .WithColumn("role_name").AsString(64).NotNullable().WithColumnDescription("角色id")
                 .WithColumn("create_user_id").AsInt64().NotNullable().WithColumnDescription("创建者id")
                 .WithColumn("create_user_name").AsString(64).NotNullable().WithColumnDescription("创建者名称")
                 .WithColumn("create_time").AsDateTime2().NotNullable().WithColumnDescription("创建时间")
                 .WithColumn("update_user_id").AsInt64().NotNullable().WithColumnDescription("最后一次修改者id")
                 .WithColumn("update_user_name").AsString(64).NotNullable().WithColumnDescription("最后一次修者名称")
                 .WithColumn("update_time").AsDateTime2().NotNullable().WithColumnDescription("最后一次修改时间");
        }
    }
}
