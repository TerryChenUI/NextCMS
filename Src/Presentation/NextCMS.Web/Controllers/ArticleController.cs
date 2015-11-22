using NextCMS.Services.Catalog;
using NextCMS.Services.Media;
using NextCMS.Web.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using NextCMS.Web.Models.Common;
using NextCMS.Core.Domain.Settings;
using NextCMS.Core;

namespace NextCMS.Web.Controllers
{
    public class ArticleController : Controller
    {
        #region 私有字段

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;
        private readonly ArticleSettings _articleSettings;

        #endregion

        #region 构造函数

        public ArticleController(IArticleService articleService, ICategoryService categoryService,
            IPictureService pictureService, ArticleSettings articleSettings)
        {
            this._articleService = articleService;
            this._categoryService = categoryService;
            this._pictureService = pictureService;
            this._articleSettings = articleSettings;
        }

        #endregion

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(int? id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var category = _categoryService.GetCategoryById(id.Value);

            var model = new ArticleListModel();
            model.CategoryId = id.HasValue ? id.Value : 0;
            model.CategoryName = category.Name;
            model.Description = category.Description;
            model.Articles = _articleService.GetAllArticle(null, model.CategoryId, 0).ToPagedList(currentPageIndex, _articleSettings.ArticlePageSize);
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var article = _articleService.GetArticleById(id);
            article.Views++;
            _articleService.UpdateArticle(article);

            var model = new ArticleDetailModel();
            model.Article = new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Author = "NextCMS",
                PictureCoverImg = _pictureService.GetPictureUrl(article.PictureId, 500, true),
                Views = article.Views,
                CommentCount = article.CommentCount,
                ShortDescription = article.ShortDescription,
                FullDescription = article.FullDescription,
                CreateDate = WebHelper.ConvertToUserTime(article.CreateDate)
            };

            if (article.Category != null)
            {
                model.Article.CategoryId = article.CategoryId.Value;
                model.Article.CategoryName = article.Category.Name;
            }

            return View(model);
        }

        /// <summary>
        /// 热门文章
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult HotArticle()
        {
            var hotTime = DateTime.Now.AddDays(-30);
            var model = _articleService.GetAllArticle(null, 0, 0).Where(t => t.CreateDate > hotTime).Take(_articleSettings.HotArticlePageSize).OrderByDescending(t => t.Views)
                .Select(t => new ArticleModel
                {
                    Id = t.Id,
                    Title = t.Title
                }).ToList();

            return PartialView("_HotArticle", model);
        }

        /// <summary>
        /// 评论排行
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CommentArticle()
        {
            var commentTime = DateTime.Now.AddDays(-30);
            var model = _articleService.GetAllArticle(null, 0, 0).Where(t => t.CreateDate > commentTime).Take(_articleSettings.HotCommentPageSize).OrderByDescending(t => t.CommentCount)
                .Select(t => new ArticleModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    CommentCount = t.CommentCount
                }).ToList();

            return PartialView("_CommentArticle", model);
        }
    }
}