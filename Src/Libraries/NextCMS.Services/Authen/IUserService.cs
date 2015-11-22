using System;
using System.Collections.Generic;
using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using System.Linq;

namespace NextCMS.Services.Authen
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial interface IUserService
    {
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
        IPagedList<User> GetAllUser(string userName = null, string email = null, 
            string phone = null, ICollection<int> roleIds = null,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据主键获取用户
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>用户对象</returns>
        User GetUserById(int id);

        /// <summary>
        /// 根据用户名获取用户对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户对象</returns>
        User GetUserByUserName(string userName);

        /// <summary>
        /// 根据电子邮箱获取用户对象
        /// </summary>
        /// <param name="userName">电子邮箱</param>
        /// <returns>用户对象</returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// 根据用户名或电子邮箱获取用户对象
        /// </summary>
        /// <param name="userNameOrEmail">用户名或电子邮箱</param>
        /// <returns>用户对象</returns>
        User GetUserByUserNameOrEmail(string userNameOrEmail);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="user">用户实体</param>
        void InsertUser(User user);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user">用户实体</param>
        void UpdateUser(User user);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user">用户实体</param>
        void DeleteUser(User user);

        #endregion

        #region 登录 ＆ 注册

        /// <summary>
        /// 验证用户注册
        /// </summary>
        /// <param name="userNameOrEmail">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserLoginResult ValidateUser(string userNameOrEmail, string password);

        #endregion

        #region 身份验证

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="rememberMe">记住我</param>
        void SignIn(User user, bool rememberMe);

        /// <summary>
        /// 退出
        /// </summary>
        void SignOut();

        /// <summary>
        /// 从Cookie获取用户
        /// </summary>
        /// <returns></returns>
        User GetAuthenticatedUser();

        #endregion
    }
}