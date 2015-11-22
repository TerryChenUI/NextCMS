using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using NextCMS.Core.Domain.Settings;
using NextCMS.Services.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NextCMS.Web.Framework
{
    /// <summary>
    /// 网站上下文实现类
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region 常用变量

        private const string UserCookieName = "NextCMS.CurrentUser";

        #endregion

        #region 私有字段

        private readonly IUserService _userService;
        private readonly GeneralSettings _generalSettings;

        #endregion

        #region 构造函数

        public WebWorkContext(IUserService userService, GeneralSettings generalSettings)
        {
            this._userService = userService;
            this._generalSettings = generalSettings;
        }

        #endregion

        protected virtual HttpCookie GetUserCookie()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
                return null;

            return HttpContext.Current.Request.Cookies[UserCookieName];
        }

        protected virtual void SetUserCookie(int userId)
        {
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                var cookie = new HttpCookie(UserCookieName);
                cookie.HttpOnly = true;
                cookie.Value = userId.ToString();
                if (userId == 0)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                HttpContext.Current.Response.Cookies.Remove(UserCookieName);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        
        public virtual GeneralSettings GeneralSettings 
        { 
            get
            {
                return _generalSettings;
            }
        }

        public virtual User CurrentUser
        {
            get
            {
                User user = null;


                user = _userService.GetAuthenticatedUser();

                //if (!user.Deleted && user.Active)
                //{
                //    SetUserCookie(user.Id);
                //}

                return user;

                ////registered user
                //if (user == null || user.Deleted || !user.Active)
                //{
                //    user = _userService.GetAuthenticatedUser();
                //}

                ////validation
                //if (!user.Deleted && user.Active)
                //{
                //    SetUserCookie(user.Id);
                //    _cachedUser = user;
                //}

                //return _cachedUser;
            }
            set{}
        }

        public virtual string UserName
        {
            get
            {
                User user =  _userService.GetAuthenticatedUser();
                if (user != null)
                {
                    return user.UserName;
                }
                else 
                {
                    return "";
                }
            }
            set { }
        }
    }
}
