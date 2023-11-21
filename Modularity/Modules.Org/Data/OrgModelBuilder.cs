using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Modules.Org.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Org.Data
{
    public class OrgModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity => 
            {
                entity.ToTable("t_company", e => e.HasComment("菜单资源表"));

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd().HasComment("id");

                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(64).HasComment("公司名");
          
                entity.Property(e => e.Description).HasColumnName("description").IsRequired().HasMaxLength(255).HasColumnName("描述");

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
