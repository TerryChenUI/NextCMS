using FluentValidation;
using NextCMS.Admin.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Validators.Catalog
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("分类名称不能为空");
        }
    }
}