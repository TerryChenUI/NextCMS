using NextCMS.Core;
using NextCMS.Core.Infrastructure;
using NextCMS.Services.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NextCMS.Web.Framework.Controllers
{
    /// <summary>
    /// 后台权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _dontValidate = false;


        public AdminAuthorizeAttribute()
            : this(false)
        {
        }

        public AdminAuthorizeAttribute(bool dontValidate)
        {
            this._dontValidate = dontValidate;
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }


        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (_dontValidate)
                return;

            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
                throw new InvalidOperationException("You cannot use [AdminAuthorize] attribute when a child action cache is active");

            if (IsAdminPageRequested(filterContext))
            {
                if (!this.HasAdminAccess(filterContext))
                    this.HandleUnauthorizedRequest(filterContext);
            }
        }

        #region

        private bool IsAdminPageRequested(AuthorizationContext filterContext)
        {
            var adminAttributes = GetAdminAuthorizeAttributes(filterContext.ActionDescriptor);
            if (adminAttributes != null && adminAttributes.Any())
                return true;
            return false;
        }

        private IEnumerable<AdminAuthorizeAttribute> GetAdminAuthorizeAttributes(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true))
                .OfType<AdminAuthorizeAttribute>();
        }

        public virtual bool HasAdminAccess(AuthorizationContext filterContext)
        {
            //TODO: 目前对Controller进行权限验证
            //var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            //var permissionService = EngineContext.Current.Resolve<IPermissionService>();
            //var workContext = EngineContext.Current.Resolve<IWorkContext>();

            //bool result = permissionService.Authorize(controller, workContext.CurrentUser);
            //return result;
            return true;
        }

        #endregion
    }
}
