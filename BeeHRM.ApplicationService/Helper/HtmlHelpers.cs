using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BeeHRM.ApplicationService.Helper
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Text Box for method
        /// </summary>
        /// <typeparam name="TModel">TModel entity</typeparam>
        /// <typeparam name="TValue">TValue entity</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">Expression field</param>
        /// <param name="value">value field</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <param name="disabled">Disabled field</param>
        /// <returns>returns mvc html string</returns>
        public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object value, object htmlAttributes, string disabled)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string[] strhtmlAttributes = htmlAttributes.ToString().Split(',');

            foreach (string parameter in strhtmlAttributes)
            {
                string[] param = parameter.Split('=');
                if (param[0].Trim() == "name")
                {
                    name = param[1].Trim();
                    break;
                }
            }

            if (string.IsNullOrEmpty(disabled))
            {
                return InputExtensions.TextBox(htmlHelper, name, value, htmlAttributes);
            }
            else
            {
                RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);

                if (disabled == "disabled")
                {
                    routeValues.Add("disabled", "disabled");
                }
                else if (disabled == "readonly")
                {
                    routeValues.Add("readonly", "read-only");
                }

                return InputExtensions.TextBox(htmlHelper, name, value, routeValues);
            }
        }

        /// <summary>
        /// Drop down list
        /// </summary>
        /// <typeparam name="TModel">TModel entity</typeparam>
        /// <typeparam name="TValue">TValue entity</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">Expression field</param>
        /// <param name="selectList">Select list</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <param name="disabled">Disabled field</param>
        /// <returns>returns mvc html string</returns>
        public static MvcHtmlString DropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, string disabled)
        {
            Func<TModel, TValue> deleg = expression.Compile();
            var result = deleg(htmlHelper.ViewData.Model);

            if (string.IsNullOrEmpty(disabled))
            {
                return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, htmlAttributes);
            }
            else
            {
                string name = ExpressionHelper.GetExpressionText(expression);

                string selectedText = SelectInternal(htmlHelper, name, selectList);

                RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);

                if (disabled == "disabled")
                {
                    routeValues.Add("disabled", "disabled");
                }
                else if (disabled == "readonly")
                {
                    routeValues.Add("readonly", "read-only");
                }

                return InputExtensions.TextBox(htmlHelper, name, selectedText, routeValues);
            }
        }

        /// <summary>
        /// Select Internal
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="name">Name string</param>
        /// <param name="selectList">Select list</param>
        /// <returns>returns string</returns>
        private static string SelectInternal(HtmlHelper htmlHelper, string name, IEnumerable selectList)
        {
            ModelState state;
            string selectedText = string.Empty;
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            object obj2 = null;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Value != null))
            {
                obj2 = state.Value.ConvertTo(typeof(string), null);
            }

            if (obj2 == null)
            {
                obj2 = htmlHelper.ViewData.Eval(fullHtmlFieldName);
            }

            if (obj2 != null)
            {
                IEnumerable source = (IEnumerable)new object[] { obj2 };
                HashSet<string> set = new HashSet<string>(source.Cast<object>().Select<object, string>(delegate (object value)
                {
                    return Convert.ToString(value, CultureInfo.CurrentCulture);
                }), StringComparer.OrdinalIgnoreCase);

                foreach (SelectListItem item in selectList)
                {
                    if ((item.Value != null) ? set.Contains(item.Value) : set.Contains(item.Text))
                    {
                        selectedText = item.Text;
                        break;
                    }
                }
            }

            return selectedText;
        }
    }
}
