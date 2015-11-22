using NextCMS.Admin.Models.Catalog;
using NextCMS.Admin.Models.Common;
using NextCMS.Services.Catalog;
using NextCMS.Services.Media;
using NextCMS.Web.Framework.Controllers;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public partial class CategoryController : BaseAdminController
    {
        #region 私有字段

        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;

        #endregion

        #region 构造函数

        public CategoryController(ICategoryService categoryService, IPictureService pictureService)
        {
            this._categoryService = categoryService;
            this._pictureService = pictureService;
        }

        #endregion

        #region 公用方法

        [NonAction]
        protected virtual void PrepareAllCategoriesModel(CategoryModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableCategories.Add(new SelectListItem()
            {
                Text = "父分类",
                Value = "0"
            });
            var permissions = _categoryService.GetAllCategory();
            foreach (var p in permissions)
            {
                model.AvailableCategories.Add(new SelectListItem()
                {
                    Text = _categoryService.GetFormattedBreadCrumb(p),
                    Value = p.Id.ToString()
                });
            }
        }

        #endregion

        #region 分类

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
            var query = _categoryService.GetAllCategory(param.iDisplayStart / param.iDisplayLength, param.iDisplayLength);
            
            var filterResult = query.Select(t =>
            {
                var category = new CategoryListModel
                {
                    Id = t.Id,
                    Name = _categoryService.GetFormattedBreadCrumb(t),
                    Published = t.Published,
                    DisplayOrder = t.DisplayOrder,
                    CreatedOnDate = t.CreatedOnDate
                };
                return category;
            });

            int sortId = param.iDisplayStart + 1;
            var result = from t in filterResult
                         select new[]
                             {
                                 sortId++.ToString(),
                                 t.Name,
                                 t.DisplayOrder.ToString(),
                                 t.Published.ToString(),
                                 t.Id.ToString()
                             };

            return DataTableJsonResult(param.sEcho, param.iDisplayStart, query.TotalCount, query.TotalCount, result);
        }

        public ActionResult Create()
        {
            var model = new CategoryModel();

            PrepareAllCategoriesModel(model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CategoryModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var category = model.ToEntity();
                category.CreatedOnDate = DateTime.Now;
                category.UpdatedOnDate = DateTime.Now;

                _categoryService.InsertCategory(category);

                SuccessNotification("添加成功");

                return continueEditing ? RedirectToAction("Edit", new { id = category.Id }) : RedirectToAction("List");
            }

            PrepareAllCategoriesModel(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            var model = category.ToModel();
            PrepareAllCategoriesModel(model);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CategoryModel model, bool continueEditing)
        {
            var category = _categoryService.GetCategoryById(model.Id);

            if (ModelState.IsValid)
            {
                int prevPictureId = category.PictureId;

                category = model.ToEntity(category);
                category.UpdatedOnDate = DateTime.Now;

                _categoryService.UpdateCategory(category);

                //图片处理, 删除旧图片
                if (prevPictureId > 0 && prevPictureId != category.PictureId)
                {
                    var prevPicture = _pictureService.GetPictureById(prevPictureId);
                    if (prevPicture != null)
                        _pictureService.DeletePicture(prevPicture);
                }

                SuccessNotification("更新成功");
                return continueEditing ? RedirectToAction("Edit", new { id = category.Id }) : RedirectToAction("List");
            }
            PrepareAllCategoriesModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            _categoryService.DeleteCategory(category);

            return Json(new { success = true });
        }

        #endregion
    }
}