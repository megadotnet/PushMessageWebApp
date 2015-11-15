// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   HtmlExtensions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    ///     HtmlExtensions
    /// </summary>
    /// <see cref="http://www.codeproject.com/Articles/419054/Practical-ASP-NET-MVC-tips" />
    public static class HtmlExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The action image.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="routeValues">
        /// The route values.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString ActionImage(
            this HtmlHelper html, 
            string imageUrl, 
            string action, 
            string controller, 
            object routeValues, 
            object htmlAttributes)
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

        /// <summary>
        /// The begin file form.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <returns>
        /// The <see cref="MvcForm"/>.
        /// </returns>
        public static MvcForm BeginFileForm(this HtmlHelper html)
        {
            return html.BeginForm(null, null, FormMethod.Post, new { enctype = "multipart/form-data" });
        }

        /// <summary>
        /// The email.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString Email(this HtmlHelper html, string name)
        {
            return html.Email(name, string.Empty);
        }

        /// <summary>
        /// The email.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString Email(this HtmlHelper html, string name, string value)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "email");
            tb.Attributes.Add("name", name);
            tb.Attributes.Add("value", value);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// The email for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString EmailFor<TModel, TProperty>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            string value = string.Empty;

            if (html.ViewContext.ViewData.Model != null)
            {
                value = expression.Compile()((TModel)html.ViewContext.ViewData.Model).ToString();
            }

            return html.Email(name, value);
        }

        /// <summary>
        /// The file.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="multiple">
        /// The multiple.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString File(this HtmlHelper html, string name, bool multiple)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);

            if (multiple)
            {
                tb.Attributes.Add("multiple", "multiple");
            }

            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// The file.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            return html.File(name, false);
        }

        /// <summary>
        /// The file for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString FileFor<TModel, TProperty>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name);
        }

        /// <summary>
        /// The multiple file for.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString MultipleFileFor<TModel, TProperty>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name, true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get full property name.
        /// </summary>
        /// <param name="exp">
        /// The exp.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
            {
                return string.Empty;
            }

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        /// <summary>
        /// The is conversion.
        /// </summary>
        /// <param name="exp">
        /// The exp.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsConversion(Expression exp)
        {
            return exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked;
        }

        /// <summary>
        /// The try find member expression.
        /// </summary>
        /// <param name="exp">
        /// The exp.
        /// </param>
        /// <param name="memberExp">
        /// The member exp.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
            {
                return true;
            }

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

                if (memberExp != null)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        // public static MvcHtmlString Captcha(this HtmlHelper htmlHelper, string textRefreshButton, int length)
        // {
        // return CaptchaHelper.GenerateFullCaptcha(htmlHelper, textRefreshButton, length);
        // }

        // public static MvcHtmlString Captcha(this HtmlHelper htmlHelper, int length)
        // {
        // return CaptchaHelper.GenerateFullCaptcha(htmlHelper, length);
        // }
    }
}