using FluentValidation;
using Modules.Role.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Validations
{
    public class CreateRoleCmdValidator: AbstractValidator<CreateRoleCmd>
    {

        public CreateRoleCmdValidator() 
        {
            ValidateRoleName();

            ValidateRoleMenuIds();
        }


        protected void ValidateRoleName()
        { 
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("210").WithMessage("角色名不能为空");

            RuleFor(x => x.Name).MaximumLength(16).WithErrorCode("211").WithMessage("角色名长度不能超过16个字符");
        }

        protected void ValidateRoleMenuIds()
        {
            RuleFor(x => x.MenuIds).Must(CheckMenuIds).WithErrorCode("212").WithMessage("请给角色分配菜单权限");
        }

        private bool CheckMenuIds(IEnumerable<long> ids)
        { 
            return ids!=null && ids.Any();
        }
    }
}
