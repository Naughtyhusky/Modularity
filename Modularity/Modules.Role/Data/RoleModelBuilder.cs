using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Modules.Role.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Data
{
    public class RoleModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Role>(entity => 
            {
                entity.ToTable("t_role", e => e.HasComment("角色表"));

                entity.HasMany(e=>e.RoleMenus).WithOne(e=>e.Role).HasPrincipalKey(e=>e.Id).HasForeignKey(e=>e.RoleId);

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd().HasComment("id");

                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(64).HasComment("角色名");

                entity.Property(e => e.Type).HasColumnName("type").IsRequired().HasComment("角色类型");

                entity.Property(e=>e.Description).HasColumnName("description").IsRequired().HasMaxLength(255).HasColumnName("描述");

                entity.Property(e => e.CreateTime).HasColumnName("create_time").HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id").HasComment("创建者id");

                entity.Property(e => e.CreateUserName).HasColumnName("create_user_name").IsRequired().HasMaxLength(64).HasComment("创建者名称");

                entity.Property(e => e.UpdateTime).HasColumnName("update_time").HasComment("最后一次修改时间");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id").HasComment("最后一次修改者id");

                entity.Property(e => e.UpdateUserName).IsRequired().HasColumnName("update_user_name").HasMaxLength(64).HasComment("最后一次修改者名称");
            });

            modelBuilder.Entity<RoleMenus>(entity => 
            {
                entity.ToTable("t_role_menus", e => e.HasComment("角色菜单关系表"));

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd().HasComment("id");

                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("role_id").HasComment("角色id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id").HasComment("菜单id");

                entity.Property(e => e.CreateTime).HasColumnName("create_time").HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id").HasComment("创建者id");

                entity.Property(e => e.CreateUserName).HasColumnName("create_user_name").IsRequired().HasMaxLength(64).HasComment("创建者名称");

                entity.Property(e => e.UpdateTime).HasColumnName("update_time").HasComment("最后一次修改时间");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id").HasComment("最后一次修改者id");

                entity.Property(e => e.UpdateUserName).IsRequired().HasColumnName("update_user_name").HasMaxLength(64).HasComment("最后一次修改者名称");
            });
        }
    }
}
