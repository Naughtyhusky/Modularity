using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Modules.Resource.Migrations
{
    [Migration(202311192100, "资源模块初始化")]
    public class Init : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
           Create.Table("t_menu").WithDescription("菜单资源表")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("id")
                 .WithColumn("name").AsString(64).NotNullable().WithColumnDescription("菜单名")
                 .WithColumn("flag").AsInt32().NotNullable().WithColumnDescription("标识")
                 .WithColumn("parent").AsInt32().NotNullable().WithColumnDescription("上级标识")
                 .WithColumn("order").AsInt32().NotNullable().WithColumnDescription("同级排序序号")
                 .WithColumn("route").AsString(64).NotNullable().WithColumnDescription("路由")
                 .WithColumn("description").AsString(255).Nullable().WithColumnDescription("描述")
                 .WithColumn("create_user_id").AsInt64().NotNullable().WithColumnDescription("创建者id")
                 .WithColumn("create_user_name").AsString(64).NotNullable().WithColumnDescription("创建者名称")
                 .WithColumn("create_time").AsDateTime2().NotNullable().WithColumnDescription("创建时间")
                 .WithColumn("update_user_id").AsInt64().NotNullable().WithColumnDescription("最后一次修改者id")
                 .WithColumn("update_user_name").AsString(64).NotNullable().WithColumnDescription("最后一次修者名称")
                 .WithColumn("update_time").AsDateTime2().NotNullable().WithColumnDescription("最后一次修改时间");

            SeedData();
        }


        public void SeedData()
        {
            Insert.IntoTable("t_menu").Row(new { name = "资源管理", flag = 1, parent = 0, order = 1, route = "", description = "资源管理", create_user_id=1, create_user_name ="管理员", create_time="2023-11-20 12:00:00", update_user_id=0, update_user_name ="", update_time="1970-01-01 00:00:00"});

            Insert.IntoTable("t_menu").Row(new { name = "用户管理", flag = 2, parent = 0, order = 2, route = "", description = "用户管理", create_user_id = 1, create_user_name = "管理员", create_time = "2023-11-20 12:00:00", update_user_id = 0, update_user_name = "", update_time = "1970-01-01 00:00:00" });

            Insert.IntoTable("t_menu").Row(new { name = "角色管理", flag = 3, parent = 0, order = 3, route = "", description = "角色管理", create_user_id = 1, create_user_name = "管理员", create_time = "2023-11-20 12:00:00", update_user_id = 0, update_user_name = "", update_time = "1970-01-01 00:00:00" });


            Insert.IntoTable("t_menu").Row(new { name = "菜单列表", flag = 4, parent = 1, order = 1, route = "", description = "菜单管理", create_user_id = 1, create_user_name = "管理员", create_time = "2023-11-20 12:00:00", update_user_id = 0, update_user_name = "", update_time = "1970-01-01 00:00:00" });

            Insert.IntoTable("t_menu").Row(new { name = "用户列表", flag = 5, parent = 2, order = 1, route = "", description = "用户列表页面", create_user_id = 1, create_user_name = "管理员", create_time = "2023-11-20 12:00:00", update_user_id = 0, update_user_name = "", update_time = "1970-01-01 00:00:00" });

            Insert.IntoTable("t_menu").Row(new { name = "用户登录记录", flag = 6, parent = 2, order = 2, route = "", description = "用户登录记录列表页面", create_user_id = 1, create_user_name = "管理员", create_time = "2023-11-20 12:00:00", update_user_id = 0, update_user_name = "", update_time = "1970-01-01 00:00:00" });

            Insert.IntoTable("t_menu").Row(new { name = "角色列表", flag = 7, parent = 3, order = 1, route = "", description = "角色列表", create_user_id = 1, create_user_name = "管理员", create_time = "2023-11-20 12:00:00", update_user_id = 0, update_user_name = "", update_time = "1970-01-01 00:00:00" });
        }
    }
}
