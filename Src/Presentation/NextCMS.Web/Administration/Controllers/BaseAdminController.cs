using NextCMS.Core;
using NextCMS.Core.Infrastructure;
using NextCMS.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    [AdminAuthorize]
    public abstract partial class BaseAdminController : BaseController
    {
        /// <summary>
        /// Initialize controller
        /// </summary>
        /// <param name="requestContext">Request context</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        /// <summary>
        /// Retuan DataTable Result
        /// </summary>
        /// <param name="sEcho"></param>
        /// <param name="iDisplayStart"></param>
        /// <param name="iTotalRecords"></param>
        /// <param name="iTotalDisplayRecords"></param>
        /// <param name="aaData"></param>
        /// <returns></returns>
        protected ActionResult DataTableJsonResult(string sEcho, int iDisplayStart, 
            int iTotalRecords, int iTotalDisplayRecords, IEnumerable<string []> aaData)
        {
            return Json(new
            {
                sEcho = sEcho,
                iDisplayStart = iDisplayStart,
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = aaData
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        protected ActionResult AccessDeniedView()
        {
            return RedirectToAction("AccessDenied", "Permission", new { pageUrl = this.Request.RawUrl });
        }
    }
}