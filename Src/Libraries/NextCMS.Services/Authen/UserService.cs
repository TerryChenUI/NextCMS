using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using NextCMS.Core;
using NextCMS.Core.Data;
using NextCMS.Core.Domain.Authen;
using System.Web;

namespace NextCMS.Services.Authen
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class UserService : IUserService
    {
        #region 字段

        private readonly IRepository<User> _userRepository;
        private readonly TimeSpan _expirationTimeSpan;

        private User _cachedUser;

        #endregion

        #region 构造函数
        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        #endregion

        #region 方法

        #region 用户

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">密码</param>
        /// <param name="phone">电话号码</param>
        /// <param name="roleIds">角色主键</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页个数</param>
        /// <returns>IQueryable User</returns>
        public virtual IPagedList<User> GetAllUser(string userName = null, string email = null,
            string phone = null, ICollection<int> roleIds = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _userRepository.Table.Where(t => !t.Deleted);

            if (!String.IsNullOrWhiteSpace(userName))
                query = query.Where(t => t.UserName.Contains(userName));

            if (!String.IsNullOrWhiteSpace(email))
                query = query.Where(t => t.Email.Contains(email));

            if (!String.IsNullOrWhiteSpace(phone))
                query = query.Where(t => t.Phone.Contains(phone));

            //取交集
            if (roleIds != null && roleIds.Count > 0)
                query = query.Where(t => t.Roles.Select(m => m.Id).Intersect(roleIds).Any());

            query = query.OrderByDescending(t => t.Id);

            return new PagedList<User>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据主键获取用户
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>用户对象</returns>
        public virtual User GetUserById(int id)
        {
            if (id == 0)
                return null;

            return _userRepository.GetById(id);
        }

        /// <summary>
        /// 根据用户名获取用户对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户对象</returns>
        public virtual User GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            return _userRepository.Table.FirstOrDefault(t => t.UserName == userName);
        }

        /// <summary>
        /// 根据电子邮箱获取用户对象
        /// </summary>
        /// <param name="userName">电子邮箱</param>
        /// <returns>用户对象</returns>
        public virtual User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return _userRepository.Table.FirstOrDefault(t => t.Email == email);
        }

        /// <summary>
        /// 根据用户名或电子邮箱获取用户对象
        /// </summary>
        /// <param name="userNameOrEmail">用户名或电子邮箱</param>
        /// <returns>用户对象</returns>
        public virtual User GetUserByUserNameOrEmail(string userNameOrEmail)
        {
            return _userRepository.Table.FirstOrDefault(t => t.UserName.ToLower() == userNameOrEmail.ToLower()
                           || t.Email.ToLower() == userNameOrEmail.ToLower());
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="user">用户实体</param>
        public virtual void InsertUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _userRepository.Insert(user);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user">用户实体</param>
        public virtual void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _userRepository.Update(user);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user">用户实体</param>
        public virtual void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.Deleted = true;

            UpdateUser(user);
        }

        #endregion

        #region 登录 &　注册

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userNameOrEmail">用户名或电子邮箱地址</param>
        /// <param name="password">密码</param>
        /// <returns>UserLoginResult</returns>
        public virtual UserLoginResult ValidateUser(string userNameOrEmail, string password)
        {
            var user = GetUserByUserNameOrEmail(userNameOrEmail);
            
            if (user == null)
                return UserLoginResult.UserNotExist;
            if (user.Deleted)
                return UserLoginResult.Deleted;
            if (!user.Active)
                return UserLoginResult.NotActive;

            //only registered can login
            //if (!user.IsRegistered())
            //    return UserLoginResult.NotRegistered;

            string pwd = "";
            //switch (user.PasswordFormat)
            //{
            //    case PasswordFormat.Encrypted:
            //        pwd = Encryption.EncryptText(password);
            //        break;
            //    case PasswordFormat.Hashed:
            //        //pwd = Encryption.CreatePasswordHash(password, user.PasswordSalt, _userSettings.HashedPasswordFormat);
            //        break;
            //    default:
            //        pwd = password;
            //        break;
            //}

            bool isValid = pwd == user.Password;

            //保存最后登陆时间
            if (isValid)
            {
                user.LastLoginDate = DateTime.Now;
                UpdateUser(user);
                return UserLoginResult.Successful;
            }
            else
            {
                return UserLoginResult.WrongPassword;
            }   
        }

        #endregion

        #region 身份验证

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="rememberMe">记住我</param>
        public virtual void SignIn(User user, bool rememberMe)
        {
            var now = DateTime.Now.ToLocalTime();

            //将用户名保存到票据中
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserName,
                now,
                //now.Add(_expirationTimeSpan),
                now.AddDays(7),
                rememberMe,
                user.UserName,
                FormsAuthentication.FormsCookiePath
                );

            //加密
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //使用Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath,
            };
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            // 将加密后的票据保存到Cookie发送到客户端
            HttpContext.Current.Response.Cookies.Add(cookie);
            _cachedUser = user;
        }

        /// <summary>
        /// 退出
        /// </summary>
        public virtual void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 从Cookie获取用户
        /// </summary>
        /// <returns></returns>
        public virtual User GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                return null;
            }

            var formsIdentity = FormsAuthentication.Decrypt(cookie.Value);

            //if (HttpContext.Current == null ||
            //    HttpContext.Current.Request == null ||
            //    !HttpContext.Current.Request.IsAuthenticated ||
            //    !(HttpContext.Current.User.Identity is FormsIdentity))
            //{
            //    return null;
            //}

            //var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
            //var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            var user = GetAuthenticatedUserFromTicket(formsIdentity);
            if (user != null && user.Active && !user.Deleted)
                _cachedUser = user;
            return _cachedUser;
        }

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var userName = ticket.UserData;

            if (String.IsNullOrWhiteSpace(userName))
                return null;

            return GetUserByUserNameOrEmail(userName);
        }
        #endregion

        #endregion
    }
}