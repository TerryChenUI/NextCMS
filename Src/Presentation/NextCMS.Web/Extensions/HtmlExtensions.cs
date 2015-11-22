//using System;
//using System.Text;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;
//using NextCMS.Core;
//using NextCMS.Core.Infrastructure;
//using NextCMS.Web.Framework.UI.Paging;
//using NextCMS.Web.Models.Common;

//namespace NextCMS.Web.Extensions
//{
//    public static class HtmlExtensions
//    {
//        //we have two pagers:
//        //The first one can have custom routes
//        //The second one just adds query string parameter
//        public static MvcHtmlString Pager<TModel>(this HtmlHelper<TModel> html, PagerModel model)
//        {
//            if (model.TotalRecords == 0)
//                return null;

//            var links = new StringBuilder();
//            if (model.ShowTotalSummary && (model.TotalPages > 0))
//            {
//                links.Append("<li class=\"total-summary\">");
//                links.Append(string.Format(model.CurrentPageText, model.PageIndex + 1, model.TotalPages, model.TotalRecords));
//                links.Append("</li>");
//            }
//            if (model.ShowPagerItems && (model.TotalPages > 1))
//            {
//                if (model.ShowFirst)
//                {
//                    //first page
//                    if ((model.PageIndex >= 3) && (model.TotalPages > model.IndividualPagesDisplayedCount))
//                    {
//                        model.RouteValues.page = 1;

//                        links.Append("<li class=\"first-page\">");
//                        if (model.UseRouteLinks)
//                        {
//                            links.Append(html.RouteLink(model.FirstButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "首页" }));
//                        }
//                        else
//                        {
//                            links.Append(html.ActionLink(model.FirstButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "首页" }));
//                        }
//                        links.Append("</li>");
//                    }
//                }
//                if (model.ShowPrevious)
//                {
//                    //previous page
//                    if (model.PageIndex > 0)
//                    {
//                        model.RouteValues.page = (model.PageIndex);

//                        links.Append("<li class=\"previous-page\">");
//                        if (model.UseRouteLinks)
//                        {
//                            links.Append(html.RouteLink(model.PreviousButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "前一页" }));
//                        }
//                        else
//                        {
//                            links.Append(html.ActionLink(model.PreviousButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "前一页" }));
//                        }
//                        links.Append("</li>");
//                    }
//                }
//                if (model.ShowIndividualPages)
//                {
//                    //individual pages
//                    int firstIndividualPageIndex = model.GetFirstIndividualPageIndex();
//                    int lastIndividualPageIndex = model.GetLastIndividualPageIndex();
//                    for (int i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
//                    {
//                        if (model.PageIndex == i)
//                        {
//                            links.AppendFormat("<li class=\"current-page\"><span>{0}</span></li>", (i + 1));
//                        }
//                        else
//                        {
//                            model.RouteValues.page = (i + 1);

//                            links.Append("<li class=\"individual-page\">");
//                            if (model.UseRouteLinks)
//                            {
//                                links.Append(html.RouteLink((i + 1).ToString(), model.RouteActionName, (object)model.RouteValues, new { title = String.Format("", (i + 1)) }));
//                            }
//                            else
//                            {
//                                links.Append(html.ActionLink((i + 1).ToString(), model.RouteActionName, (object)model.RouteValues, new { title = String.Format("", (i + 1)) }));
//                            }
//                            links.Append("</li>");
//                        }
//                    }
//                }
//                if (model.ShowNext)
//                {
//                    //next page
//                    if ((model.PageIndex + 1) < model.TotalPages)
//                    {
//                        model.RouteValues.page = (model.PageIndex + 2);

//                        links.Append("<li class=\"next-page\">");
//                        if (model.UseRouteLinks)
//                        {
//                            links.Append(html.RouteLink(model.NextButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "下一页" }));
//                        }
//                        else
//                        {
//                            links.Append(html.ActionLink(model.NextButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "下一页" }));
//                        }
//                        links.Append("</li>");
//                    }
//                }
//                if (model.ShowLast)
//                {
//                    //last page
//                    if (((model.PageIndex + 3) < model.TotalPages) && (model.TotalPages > model.IndividualPagesDisplayedCount))
//                    {
//                        model.RouteValues.page = model.TotalPages;

//                        links.Append("<li class=\"last-page\">");
//                        if (model.UseRouteLinks)
//                        {
//                            links.Append(html.RouteLink(model.LastButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "最后一页" }));
//                        }
//                        else
//                        {
//                            links.Append(html.ActionLink(model.LastButtonText, model.RouteActionName, (object)model.RouteValues, new { title = "最后一页" }));
//                        }
//                        links.Append("</li>");
//                    }
//                }
//            }
//            var result = links.ToString();
//            if (!String.IsNullOrEmpty(result))
//            {
//                result = "<ul>" + result + "</ul>";
//            }
//            return MvcHtmlString.Create(result);
//        }
        
//        public static Pager Pager(this HtmlHelper helper, IPageableModel pagination)
//        {
//            return new Pager(pagination, helper.ViewContext);
//        }
//        public static Pager Pager(this HtmlHelper helper, string viewDataKey)
//        {
//            var dataSource = helper.ViewContext.ViewData.Eval(viewDataKey) as IPageableModel;

//            if (dataSource == null)
//            {
//                throw new InvalidOperationException(string.Format("Item in ViewData with key '{0}' is not an IPagination.",
//                                                                  viewDataKey));
//            }

//            return helper.Pager(dataSource);
//        }

//    }
//}

