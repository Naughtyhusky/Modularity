using FluentValidation;
using Modules.User.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Validations
{
    public class CreateUserCmdValidator: AbstractValidator<CreateUserCmd>
    {

        public CreateUserCmdValidator()
        {
            ValidateAccount();

            ValidatePassword();

            ValidateName();
        }


        protected void ValidateAccount()
        { 
            RuleFor(x=>x.Account).NotNull().NotEmpty().WithErrorCode("110").WithMessage("账号不能为空");

            RuleFor(x => x.Account).MaximumLength(32).WithErrorCode("111").WithMessage("账号长度不能超过32个字符");

            RuleFor(x=>x.Account).Must(IsLetterDigit).WithErrorCode("112").WithMessage("账号只能使用字母和数字");
        }


        protected void ValidatePassword()
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().WithErrorCode("113").WithMessage("密码不能为空");

            RuleFor(x => x.Password).MaximumLength(16).WithErrorCode("114").WithMessage("密码长度不能超过个16位");
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithErrorCode("115").WithMessage("用户名不能为空");

            RuleFor(x => x.Name).MaximumLength(32).WithErrorCode("116").WithMessage("用户名长度不能超过个32个字符");
        }



        private bool IsLetterDigit(string str)
        { 
            return str.IsLetterDigit();
        }
    }
}
