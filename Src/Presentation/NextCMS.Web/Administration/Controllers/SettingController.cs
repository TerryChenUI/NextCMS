using NextCMS.Admin.Models.Settings;
using NextCMS.Core.Domain.Settings;
using NextCMS.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public partial class SettingController : BaseAdminController
    {
        #region 私有字段

        private readonly ISettingService _settingService;

        #endregion

        #region 构造函数

        public SettingController(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        #endregion

        #region 文章设置

        public ActionResult Article()
        {
            var articleSettings = _settingService.LoadSetting<ArticleSettings>();

            var model = articleSettings.ToModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Article(ArticleSettingsModel model)
        {
            var articleSettings = _settingService.LoadSetting<ArticleSettings>();
            articleSettings = model.ToEntity(articleSettings);

            _settingService.SaveSetting(articleSettings, x => x.ArticlePageSize, false);
            _settingService.SaveSetting(articleSettings, x => x.HotArticlePageSize, false);
            _settingService.SaveSetting(articleSettings, x => x.HotCommentPageSize, false);
            _settingService.SaveSetting(articleSettings, x => x.LatestCommentPageSize, false);
            _settingService.SaveSetting(articleSettings, x => x.CommentPageSize, false);
            _settingService.SaveSetting(articleSettings, x => x.AllowComment, false);

            SuccessNotification("保存成功");

            return RedirectToAction("Article");
        }

        #endregion

        #region 网站设置

        public ActionResult GeneralCommon()
        {
            var generalSettings = _settingService.LoadSetting<GeneralSettings>();

            var model = generalSettings.ToModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult GeneralCommon(GeneralSettingsModel model)
        {
            var general = _settingService.LoadSetting<GeneralSettings>();
            general = model.ToEntity(general);

            _settingService.SaveSetting(general, x => x.SiteTitle, false);
            _settingService.SaveSetting(general, x => x.Separator, false);
            _settingService.SaveSetting(general, x => x.MetaTitle, false);
            _settingService.SaveSetting(general, x => x.MetaKeywords, false);
            _settingService.SaveSetting(general, x => x.MetaDescription, false);

            SuccessNotification("保存成功");

            return RedirectToAction("GeneralCommon");
        }

        #endregion
    }
}