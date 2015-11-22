using AutoMapper;
using NextCMS.Admin.Models.Authen;
using NextCMS.Admin.Models.Catalog;
using NextCMS.Admin.Models.Settings;
using NextCMS.Core.Domain.Authen;
using NextCMS.Core.Domain.Catalog;
using NextCMS.Core.Domain.Settings;
using NextCMS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            //角色
            Mapper.CreateMap<RoleModel, Role>();
            Mapper.CreateMap<Role, RoleModel>();

            //权限
            Mapper.CreateMap<PermissionModel, Permission>();
            Mapper.CreateMap<Permission, PermissionModel>();

            //文章
            Mapper.CreateMap<ArticleModel, Article>()
                .ForMember(dest => dest.CreateDate, mo => mo.Ignore())
                .ForMember(dest => dest.UpdateDate, mo => mo.Ignore())
                .ForMember(dest => dest.Deleted, mo => mo.Ignore());

            Mapper.CreateMap<Article, ArticleModel>()
               .ForMember(dest => dest.Categories, mo => mo.Ignore())
               .ForMember(dest => dest.Tags, mo => mo.Ignore())
               .ForMember(dest => dest.SelectedTags, mo => mo.Ignore());

            //分类
            Mapper.CreateMap<CategoryModel, Category>()
                .ForMember(dest => dest.CreatedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.Deleted, mo => mo.Ignore());

            Mapper.CreateMap<Category, CategoryModel>();

            //标签
            Mapper.CreateMap<TagModel, Tag>();
            Mapper.CreateMap<Tag, TagModel>();

            //设置
            Mapper.CreateMap<ArticleSettingsModel, ArticleSettings>();
            Mapper.CreateMap<ArticleSettings, ArticleSettingsModel>();
            Mapper.CreateMap<GeneralSettingsModel, GeneralSettings>();
            Mapper.CreateMap<GeneralSettings, GeneralSettingsModel>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}