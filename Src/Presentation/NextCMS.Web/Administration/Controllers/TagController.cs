using NextCMS.Admin.Models.Catalog;
using NextCMS.Admin.Models.Common;
using NextCMS.Services.Catalog;
using NextCMS.Web.Framework.Controllers;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public partial class TagController : BaseAdminController
    {
        #region 私有字段

        private readonly IArticleService _articleService;

        #endregion

        #region 构造函数

        public TagController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        #endregion

        #region 标签

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult InitDataTable(DataTableParameter param)
        {
            var query = _articleService.GetAllTag(param.iDisplayStart / param.iDisplayLength, param.iDisplayLength);

            var filterResult = query.Select(t => new TagModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            });

            int sortId = param.iDisplayStart + 1;
            var result = from t in filterResult
                         select new[]
                             {
                                 sortId++.ToString(),
                                 t.Name,
                                 t.Description,
                                 t.Id.ToString()
                             };

            return DataTableJsonResult(param.sEcho, param.iDisplayStart, query.TotalCount, query.TotalCount, result);
        }

        public ActionResult Create()
        {
            var model = new TagModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(TagModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var tag = model.ToEntity();
                _articleService.InsertTag(tag);

                SuccessNotification("添加成功");

                return continueEditing ? RedirectToAction("Edit", new { id = tag.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var tag = _articleService.GetTagById(id);
            var model = tag.ToModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(TagModel model, bool continueEditing)
        {
            var tag = _articleService.GetTagById(model.Id);

            if (ModelState.IsValid)
            {
                tag = model.ToEntity(tag);
                _articleService.UpdateTag(tag);

                SuccessNotification("保存成功");
                return continueEditing ? RedirectToAction("Edit", new { id = tag.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var tag = _articleService.GetTagById(id);
            _articleService.DeleteTag(tag);

            return Json(new { success = true });
        }

        #endregion
    }
}