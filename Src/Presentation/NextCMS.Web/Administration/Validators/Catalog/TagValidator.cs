using FluentValidation;
using FluentValidation.Attributes;
using NextCMS.Admin.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Validators.Catalog
{
    public class TagValidator: AbstractValidator<TagModel>
    {
        public TagValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("标签名称不能为空");
        }
    }
}