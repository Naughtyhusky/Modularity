using FluentValidation;
using Modules.Org.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Org.Validations
{
    public class CreateCompanyCmdValidator : AbstractValidator<CreateCompanyCmd>
    {

        public CreateCompanyCmdValidator() 
        {
            ValidateName();
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("410").WithMessage("公司名不能为空");

            RuleFor(x => x.Name).MaximumLength(32).WithErrorCode("411").WithMessage("公司名长度不能超过32个字符");
        }
    }
}
