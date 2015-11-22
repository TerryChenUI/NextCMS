using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Transactions;

namespace NextCMS.Data.Initializers
{
    public class CreateCeDatabaseIfNotExists : CreateDatabaseIfNotExists<NextCMSObjectContext>
    {
        protected override void Seed(NextCMSObjectContext context)
        {
            var roles = new List<Role>
            {
                new Role{ Name = "超级管理员", SystemName = "SupperManager", Active = true, IsSystemRole = true },
                new Role{ Name = "管理员", SystemName = "Manager", Active = true, IsSystemRole = true },
                new Role{ Name = "注册会员", SystemName = "Member", Active = true, IsSystemRole = true }
            };

            var roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();

            var user = new List<User>
            {
                new User
                { 
                    UserName = "admin",
                    Email = "admin@qq.com",
                    Phone = "13726280898",
                    Password = Encryption.EncryptText("admin"),
                    RegisterDate = DateTime.Now,
                    LastActivityDate = DateTime.Now
                }
            };

            base.Seed(context);
        }
    }


}
