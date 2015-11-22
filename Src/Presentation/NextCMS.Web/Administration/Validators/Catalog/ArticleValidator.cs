using FluentValidation;
using NextCMS.Admin.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Validators.Catalog
{
    public class ArticleValidator : AbstractValidator<ArticleModel>
    {
        public ArticleValidator()
        {
            RuleFor(t => t.Title).NotEmpty().WithMessage("标题不能为空");
            RuleFor(t => t.ShortDescription).NotEmpty().WithMessage("简短描述不能为空");
            RuleFor(t => t.FullDescription).NotEmpty().WithMessage("正文不能为空");
        }
    }
}