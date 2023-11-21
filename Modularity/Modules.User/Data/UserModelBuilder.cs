using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Modules.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Data
{
    public class UserModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.User>(entity => 
            {
                entity.ToTable("t_user",tb=>tb.HasComment("用户信息表"));
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Account).IsUnique();
         
                entity.Property(e => e.Id).HasColumnName("id");
             
                entity.Property(e=>e.Account).HasColumnName("account").IsRequired().HasMaxLength(32).HasComment("登录账号");

                entity.Property(e=>e.Password).HasColumnName("password").IsRequired().HasMaxLength(64).HasComment("密码密文");

                entity.Property(e => e.Salt).HasColumnName("salt").IsRequired().HasMaxLength(64).HasComment("盐");

                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(64).HasComment("用户名");

                entity.Property(e => e.Gender).HasColumnName("gender").IsRequired().HasComment("性别");

                entity.Property(e => e.State).HasColumnName("state").IsRequired().HasComment("账号状态");

                entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired().HasComment("角色id");

                entity.Property(e => e.RoleName).HasColumnName("role_name").IsRequired().HasMaxLength(64).HasComment("角色id");

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
