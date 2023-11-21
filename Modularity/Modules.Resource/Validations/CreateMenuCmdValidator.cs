using FluentValidation;
using Modules.Resource.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Resource.Validations
{
    public class CreateMenuCmdValidator : AbstractValidator<CreateMenuCmd>
    {
        public CreateMenuCmdValidator() 
        {
            ValidateName();

            ValidateRoute();
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("310").WithMessage("菜单名不能为空");

            RuleFor(x => x.Name).MaximumLength(32).WithErrorCode("311").WithMessage("菜单名长度不能超过32个字符");
        }

        protected void ValidateRoute() 
        {
            RuleFor(x => x.Route).NotEmpty().WithErrorCode("312").WithMessage("路由不能为空");

            RuleFor(x => x.Route).MaximumLength(64).WithErrorCode("313").WithMessage("路由长度不能超过64个字符");
        }
    }
}
