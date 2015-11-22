using FluentValidation;
using NextCMS.Admin.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Validators.Authen
{
    public class RoleValidator: AbstractValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("角色名称不能为空");
            RuleFor(t => t.SystemName).NotEmpty().WithMessage("系统角色不能为空");
        }
    }
}