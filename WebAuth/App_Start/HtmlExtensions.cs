using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebAuth.App_Start
{
    /// <summary>
    /// HtmlExtensions
    /// </summary>
    /// <see cref="http://www.codeproject.com/Articles/419054/Practical-ASP-NET-MVC-tips"/>
    public static class HtmlExtensions
    {
        public static MvcForm BeginFileForm(this HtmlHelper html)
        {
            return html.BeginForm(null, null, FormMethod.Post, new { enctype = "multipart/form-data" });
        }

        public static MvcHtmlString File(this HtmlHelper html, string name, bool multiple)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);

            if (multiple)
                tb.Attributes.Add("multiple", "multiple");

            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            return html.File(name, false);
        }

        public static MvcHtmlString MultipleFileFor<TModel, TProperty>(this HtmlHelper<TModel> html,
               Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name, true);
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html,
                      Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name);
        }

        public static MvcHtmlString ActionImage(this HtmlHelper html, string imageUrl,
               string action, string controller, object routeValues, object htmlAttributes)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var link = new TagBuilder("a");
            link.Attributes.Add("href", urlHelper.Action(action, controller, routeValues));
            var img = new TagBuilder("img");
            img.Attributes.Add("src", imageUrl);
            img.Attributes.Add("alt", action);
            link.InnerHtml = img.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Email(this HtmlHelper html, string name)
        {
            return html.Email(name, string.Empty);
        }

        public static MvcHtmlString Email(this HtmlHelper html, string name, string value)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "email");
            tb.Attributes.Add("name", name);
            tb.Attributes.Add("value", value);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString EmailFor<TModel, TProperty>(this HtmlHelper<TModel> html,
               Expression<Func<TModel, TProperty>> expression)
        {
            var name = GetFullPropertyName(expression);
            var value = string.Empty;

            if (html.ViewContext.ViewData.Model != null)
                value = expression.Compile()((TModel)html.ViewContext.ViewData.Model).ToString();

            return html.Email(name, value);
        }

        static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

                if (memberExp != null)
                    return true;
            }

            return false;
        }

        static bool IsConversion(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
        }

        //public static MvcHtmlString Captcha(this HtmlHelper htmlHelper, string textRefreshButton, int length)
        //{
        //    return CaptchaHelper.GenerateFullCaptcha(htmlHelper, textRefreshButton, length);
        //}

        //public static MvcHtmlString Captcha(this HtmlHelper htmlHelper, int length)
        //{
        //    return CaptchaHelper.GenerateFullCaptcha(htmlHelper, length);
        //}
    }
}