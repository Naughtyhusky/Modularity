using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Migrations
{
    [Migration(202311191900, "角色模块初始化")]
    public class Init : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
              Create.Table("t_role").WithDescription("角色表")
                    .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("id")
                    .WithColumn("name").AsString(64).NotNullable().WithColumnDescription("角色名")
                    .WithColumn("type").AsInt32().NotNullable().WithColumnDescription("角色类型")
                    .WithColumn("description").AsString(255).Nullable().WithColumnDescription("描述")
                    .WithColumn("create_user_id").AsInt64().NotNullable().WithColumnDescription("创建者id")
                    .WithColumn("create_user_name").AsString(64).NotNullable().WithColumnDescription("创建者名称")
                    .WithColumn("create_time").AsDateTime2().NotNullable().WithColumnDescription("创建时间")
                    .WithColumn("update_user_id").AsInt64().NotNullable().WithColumnDescription("最后一次修改者id")
                    .WithColumn("update_user_name").AsString(64).NotNullable().WithColumnDescription("最后一次修者名称")
                    .WithColumn("update_time").AsDateTime2().NotNullable().WithColumnDescription("最后一次修改时间");


            Create.Table("t_role_menus").WithDescription("角色菜单关系表")
                  .WithColumn("role_id").AsInt64().NotNullable().WithColumnDescription("角色id").Indexed()
                  .WithColumn("menu_id").AsInt64().NotNullable().WithColumnDescription("菜单id")
                  .WithColumn("create_user_id").AsInt64().NotNullable().WithColumnDescription("创建者id")
                  .WithColumn("create_user_name").AsString(64).NotNullable().WithColumnDescription("创建者名称")
                  .WithColumn("create_time").AsDateTime2().NotNullable().WithColumnDescription("创建时间")
                  .WithColumn("update_user_id").AsInt64().NotNullable().WithColumnDescription("最后一次修改者id")
                  .WithColumn("update_user_name").AsString(64).NotNullable().WithColumnDescription("最后一次修者名称")
                  .WithColumn("update_time").AsDateTime2().NotNullable().WithColumnDescription("最后一次修改时间");
        }
    }
}
