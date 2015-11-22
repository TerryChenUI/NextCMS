using FluentValidation;
using NextCMS.Admin.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Validators.Authen
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(t => t.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("电子邮箱不能为空")
                .EmailAddress().WithMessage("邮箱格式不正确");;
            RuleFor(t => t.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}