using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace NextCMS.Web.Framework
{
    public static partial class HtmlHelperExtend
    {
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            DisplayDirection direction)
        {
            string checkPropertyName;
            string displayPropertyName;

            MemberExpression nameExpr = (MemberExpression)expression.Body;
            checkPropertyName = nameExpr.Member.Name;

            PropertyInfo property = typeof(TModel).GetProperty(checkPropertyName);
            KeyValueAttribute attribute = (KeyValueAttribute)property.GetCustomAttributes(typeof(KeyValueAttribute), false)[0];

            if (attribute == null)
            {
                throw new Exception();
            }

            displayPropertyName = attribute.DisplayProperty;

            IEnumerable checkList = (IEnumerable)htmlHelper.ViewData.Eval(displayPropertyName);
            var isCheckedList = htmlHelper.ViewData.Eval(checkPropertyName) as IEnumerable;

            #region Build html string

            var htmlStr = new StringBuilder("<div class=\"checkbox-list\">");

            int index = 1;

            foreach (KeyValueModel check in checkList)
            {
                if (direction == DisplayDirection.Horizon)
                {
                    htmlStr.Append("<label class=\"checkbox-inline\">");
                }
                else
                {
                    htmlStr.Append("<label>");
                }

                string checkedStr = "";
                bool isCheck = false;
                foreach (var checkvalue in isCheckedList)
                {
                    isCheck = check.Value == checkvalue.ToString();
                    if (isCheck)
                    {
                        checkedStr = "checked=\"checked\"";
                        break;
                    }
                }
                KeyValueModel checkModel = (KeyValueModel)check;
                if (check.Disable == "disabled" && check.Disable != null)
                {
                    htmlStr.Append(string.Format("<input type=\"checkbox\" name=\"{0}\" disabled=\"{1}\" value=\"{2}\" {3} /> ", checkPropertyName, check.Disable, check.Value, checkedStr));
                    
                }
                else
                {
                    htmlStr.Append(string.Format("<input type='checkbox' name='{0}' value='{1}' {2} /> ", checkPropertyName, check.Value, checkedStr));
                }

                htmlStr.Append(checkModel.Text + "</label>");
                index = index + 1;
            }
            htmlStr.Append("</div>");

            #endregion

            MvcHtmlString result = new MvcHtmlString(htmlStr.ToString());
            return result;
        }

        //public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DisplayDirection direction, object htmlAttribute, object itemAttribute)
        //{
        //    return CheckBoxListFor(htmlHelper, expression,direction, 2, htmlAttribute, itemAttribute);
        //}

        //public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DisplayDirection direction, object htmlAttribute)
        //{
        //    return CheckBoxListFor(htmlHelper, expression, direction, 2, null, new {style="display:inline-block;width:70px;" });
        //}
    }
}