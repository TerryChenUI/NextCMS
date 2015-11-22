using FluentValidation.Attributes;
using NextCMS.Admin.Validators.Catalog;
using NextCMS.Web.Framework;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Models.Catalog
{
    [Validator(typeof(ArticleValidator))]
    public class ArticleModel : BaseNextCMSModel
    {
        public ArticleModel()
        {
            this.Categories = new List<SelectListItem> { 
                new SelectListItem { Text = "请选择", Value = "0"}
            };

            this.Tags = new List<KeyValueModel>();
            this.SelectedTags = new List<int>();
        }

        public string Title { get; set; }

        [AllowHtml]
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string FullDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public int PictureId { get; set; }

        public int Views { get; set; }

        public int CommentCount { get; set; }

        public int Like { get; set; }

        public int Hate { get; set; }

        public bool AllowComment { get; set; }

        public bool ShowOnTop { get; set; }

        public bool Published { get; set; }

        public string CreatedOnDate { get; set; }

        public string UpdatedOnDate { get; set; }


        #region 分类

        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        #endregion

        #region 标签

        public ICollection<KeyValueModel> Tags { get; set; }

        [KeyValue(DisplayProperty = "Tags")]
        public ICollection<int> SelectedTags { get; set; }

        #endregion
    }
}