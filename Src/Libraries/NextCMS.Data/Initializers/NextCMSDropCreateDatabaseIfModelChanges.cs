using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Transactions;

namespace NextCMS.Data.Initializers
{
    /// </summary>
    public class NextCMSDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<NextCMSObjectContext>
    {
        protected override void Seed(NextCMSObjectContext context)
        {
            #region 角色
            var roles = new List<Role>
            {
                new Role{ Name = "注册会员", SystemName = "Member", Active = true, IsSystemRole = false }
            };

            var roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();
            #endregion

            #region 用户
            var users = new List<User>
            {
                new User{
                    UserName = "test",
                    Email = "test@qq.com",
                    Phone = "13726280898",
                    Password = Encryption.EncryptText("test"),
                    RegisterDate = DateTime.Now,
                    LastActivityDate = DateTime.Now
                }
            };

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@qq.com",
                Phone = "13726280898",
                Password = Encryption.EncryptText("admin"),
                RegisterDate = DateTime.Now,
                LastActivityDate = DateTime.Now
            };
            admin.Roles.Add(new Role { Name = "超级管理员", SystemName = "SupperManager", Active = true, IsSystemRole = true });
            users.Add(admin);

            var userSet = context.Set<User>();
            userSet.AddOrUpdate(m => new { m.UserName }, users.ToArray());
            context.SaveChanges();

            #endregion

            #region 权限

            var permissions = new List<Permission> { 
                new Permission{ Id = 1, Name = "网站管理", ParentId = 0, Area = null, Controller = null, Action = null, Icon = "fa-adjust",  DisplayOrder = 1, Active = true, Deleted = false },
                new Permission{ Id = 2, Name = "角色管理", ParentId = 1, Area = "Admin", Controller = "Role", Action = "List", Icon = "fa-anchor",  DisplayOrder = 1, Active = true, Deleted = false },
                new Permission{ Id = 3, Name = "用户管理", ParentId = 1, Area = "Admin", Controller = "User", Action = "List", Icon = "fa-anchor",  DisplayOrder = 2, Active = true, Deleted = false },
                new Permission{ Id = 4, Name = "访问控制", ParentId = 1, Area = "Admin", Controller = "Permission", Action = "AccessControl", Icon = "",  DisplayOrder = 3, Active = true, Deleted = false },
                new Permission{ Id = 5, Name = "内容管理", ParentId = 0, Area = null, Controller = null, Action = null, Icon = "",  DisplayOrder = 2, Active = true, Deleted = false },
                new Permission{ Id = 6, Name = "类别管理", ParentId = 5, Area = "Admin", Controller = "Category", Action = "List", Icon = "",  DisplayOrder = 1, Active = true, Deleted = false },
                new Permission{ Id = 7, Name = "文章管理", ParentId = 5, Area = "Admin", Controller = "Article", Action = "List", Icon = "fa-asterisk",  DisplayOrder = 2, Active = true, Deleted = false }
            };

            var permissionSet = context.Set<Permission>();
            permissionSet.AddOrUpdate(m => new { m.Id }, permissions.ToArray());
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}
