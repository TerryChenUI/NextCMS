using NextCMS.Services.Authen;
using NextCMS.Services.Catalog;
using NextCMS.Services.Media;
using NextCMS.Web.Models.Articles;
using NextCMS.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using NextCMS.Core.Domain.Settings;

namespace NextCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        #region 私有字段

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;
        private readonly ArticleSettings _articleSettings;

        #endregion

        #region 构造函数

        public HomeController(IArticleService articleService, ICategoryService categoryService,
            IPictureService pictureService, ArticleSettings articleSettings)
        {
            this._articleService = articleService;
            this._categoryService = categoryService;
            this._pictureService = pictureService;
            this._articleSettings = articleSettings;
        }

        #endregion

        public ActionResult Index(int? page)
        {        
            var model = new HomeModel();

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.Articles = _articleService.GetAllArticle(null, 0, 0).ToList().ToPagedList(currentPageIndex, _articleSettings.ArticlePageSize);

            #region 标签

            #endregion

            #region 置顶推荐

            model.RecommandArticles = _articleService.GetAllArticle(null, 0, 0).Where(t => t.ShowOnTop).Take(4).OrderByDescending(t => t.Views)
                .ToList().Select(t =>
                {
                    var article = new ArticleModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        ShortDescription = t.ShortDescription
                    };
                    article.PictureCoverImg = _pictureService.GetPictureUrl(t.PictureId, 100, true);
                    return article;
                });

            #endregion

            #region 热门文章

            var hotTime = DateTime.Now.AddDays(-30);
            model.HotArticles = _articleService.GetAllArticle(null, 0, 0).Where(t => t.CreateDate > hotTime).Take(_articleSettings.HotArticlePageSize).OrderByDescending(t => t.Views)
                .Select(t => new ArticleModel
                    {
                        Id = t.Id,
                        Title = t.Title
                    }).ToList();

            #endregion

            #region 评论排行

            var commentTime = DateTime.Now.AddDays(-30);
            model.CommentArticles = _articleService.GetAllArticle(null, 0, 0).Where(t => t.CreateDate > commentTime).Take(_articleSettings.HotCommentPageSize).OrderByDescending(t => t.CommentCount)
                .Select(t => new ArticleModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    CommentCount = t.CommentCount
                }).ToList();

            #endregion

            return View(model);
        }
    }
}