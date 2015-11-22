using NextCMS.Admin.Models.Catalog;
using NextCMS.Admin.Models.Common;
using NextCMS.Core;
using NextCMS.Core.Domain.Catalog;
using NextCMS.Services.Catalog;
using NextCMS.Services.Media;
using NextCMS.Web.Framework;
using NextCMS.Web.Framework.Controllers;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public partial class ArticleController : BaseAdminController
    {
        #region 私有字段

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;

        #endregion

        #region 构造函数

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IPictureService pictureService)
        {
            this._articleService = articleService;
            this._categoryService = categoryService;
            this._pictureService = pictureService;
        }

        #endregion

        #region 公共方法

        [NonAction]
        protected virtual void PrepareModel(ArticleModel model, Article article = null)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            //所有分类
            model.Categories.AddRange(
                _categoryService.GetAllCategory().Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                })
            );

            //所有标签
            model.Tags = _articleService.GetAllTag().Select(t => new KeyValueModel
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            //选中标签
            if (article != null)
            {
                model.SelectedTags = article.Tags.Select(t => t.Id).ToList();
            }
            
        }

        #endregion

        #region 文章

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ArticleSearchModel();
            model.SearchCategories.AddRange(
                _categoryService.GetAllCategory().Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                })
            );
            return View(model);
        }

        public ActionResult InitDataTable(ArticleSearchModel model)
        {
            var query = _articleService.GetAllArticle(title: model.SearchTitle, categoryId: model.SearchCategoryId, showHidden: true,
                pageIndex: model.iDisplayStart / model.iDisplayLength, pageSize: model.iDisplayLength);

            var filterResult = query.Select(t => new ArticleListModel
            {
                Id = t.Id,
                Title = t.Title,
                Views = t.Views,
                CommentCount = t.CommentCount,
                Published = t.Published,
                CreateDate = WebHelper.ConvertToUserTime(t.CreateDate)
            });

            int sortId = model.iDisplayStart + 1;
            var result = from t in filterResult
                         select new[]
                             {
                                 sortId++.ToString(),
                                 t.Title,
                                 t.CreateDate,
                                 t.Views.ToString(),
                                 t.CommentCount.ToString(),
                                 t.Published.ToString(),
                                 t.Id.ToString(),
                                 t.Id.ToString(),
                             };

            return DataTableJsonResult(model.sEcho, model.iDisplayStart, query.TotalCount, query.TotalCount, result);
        }

        public ActionResult Create()
        {
            var model = new ArticleModel();
            PrepareModel(model);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ArticleModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var article = model.ToEntity();
                article.CreateDate = DateTime.Now;
                article.UpdateDate = DateTime.Now;

                if (model.CategoryId == 0)
                    article.CategoryId = null;

                //标签
                foreach (var id in model.SelectedTags)
                {
                    article.Tags.Add(_articleService.GetTagById(id));
                }

                _articleService.InsertArticle(article);

                SuccessNotification("添加成功");

                return continueEditing ? RedirectToAction("Edit", new { id = article.Id }) : RedirectToAction("List");
            }

            PrepareModel(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var article = _articleService.GetArticleById(id);
            var model = article.ToModel();
            PrepareModel(model, article);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ArticleModel model, bool continueEditing)
        {
            var article = _articleService.GetArticleById(model.Id);

            if (ModelState.IsValid)
            {
                int prevPictureId = article.PictureId;

                article = model.ToEntity(article);
                article.UpdateDate = DateTime.Now;

                if (model.CategoryId == 0)
                    article.CategoryId = null;

                //标签
                var allTags = _articleService.GetAllTag().ToList();
                foreach (var tag in allTags)
                {
                    if (model.SelectedTags != null && model.SelectedTags.Contains(tag.Id))
                    {
                        if (article.Tags.Count(t => t.Id == tag.Id) == 0)
                        {
                            article.Tags.Add(tag);
                        }
                    }
                    else
                    {
                        if (article.Tags.Count(t => t.Id == tag.Id) > 0)
                        {
                            article.Tags.Remove(tag);
                        }
                    }
                }

                _articleService.UpdateArticle(article);

                //图片处理, 删除旧图片
                if (prevPictureId > 0 && prevPictureId != article.PictureId)
                {
                    var prevPicture = _pictureService.GetPictureById(prevPictureId);
                    if (prevPicture != null)
                        _pictureService.DeletePicture(prevPicture);
                }

                SuccessNotification("保存成功");
                return continueEditing ? RedirectToAction("Edit", new { id = article.Id }) : RedirectToAction("List");
            }

            PrepareModel(model, article);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var article = _articleService.GetArticleById(id);
            _articleService.DeleteArticle(article);

            return Json(new { success = true });
        }

        #endregion
    }
}