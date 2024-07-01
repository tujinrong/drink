using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.DAL;
using WebEvaluation.Models;

namespace System.Web.Mvc.Html
{
    public static class ReferExtensions
    {
        public static MvcHtmlString CodeName(this HtmlHelper htmlHelper, string name, string referKey, object value)
        {
            return CodeName(htmlHelper, name, referKey, value, null);
        }

        public static MvcHtmlString CodeName(this HtmlHelper htmlHelper, string name, string referKey, object value, object htmlAttributes)
        {
            var sb = new StringBuilder();

            string uuid = System.Guid.NewGuid().ToString();

            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            if (attrs.ContainsKey(@"class"))
            {
                if (("" + attrs[@"class"]).Contains("form-control input-sm") == false)
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " form-control input-sm";
                }
            }
            else
            {
                attrs[@"class"] = " form-control input-sm";
            } 

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(attrs);
            tagBuilder.MergeAttribute("type", "text");
            tagBuilder.MergeAttribute("name", name);
            tagBuilder.MergeAttribute("uuid", uuid);
            tagBuilder.MergeAttribute("id", name);
            if (value != null)
            {
                tagBuilder.MergeAttribute("value", value.ToString());
            }
            tagBuilder.MergeAttribute("id", name);

            sb.Append(@"<div class='input-group shop' >" + tagBuilder.ToString() + "<span id='btn_" + name + "' class='input-group-addon input-sm'><i class='glyphicon glyphicon-search' style='cursor: pointer;'></i></span></div>");
            sb.Append("<script type=\"text/javascript\">");
            sb.Append(" new SMAT.CodeName({field:'" + name + "',referKey:'" + referKey + "',uuid:'" + uuid + "'})");
            sb.Append("</script>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString CodeNameFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, string name, string referKey, Expression<Func<TModel, TValue>> expression)
        {
            var sb = new StringBuilder();

            string valueStr = htmlHelper.DisplayFor(expression).ToString();

            return CodeName(htmlHelper, name, referKey, valueStr);
        }

        public static MvcHtmlString CodeNameFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, string name, string referKey, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var sb = new StringBuilder();

            string valueStr = htmlHelper.DisplayFor(expression).ToString();

            return CodeName(htmlHelper, name, referKey, valueStr, htmlAttributes);
        }

        public static MvcHtmlString CodeNameLabel(this HtmlHelper htmlHelper, string name)
        {
            return CodeNameLabel(htmlHelper, name, null);
        }

        public static MvcHtmlString CodeNameLabel(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            string uuid = System.Guid.NewGuid().ToString();

            var sb = new StringBuilder();
            TagBuilder tagBuilder = new TagBuilder("label");
            tagBuilder.MergeAttributes(attrs);
            tagBuilder.MergeAttribute("uuid", uuid);
            tagBuilder.MergeAttribute("id", name + "_text");
            tagBuilder.MergeAttribute("name", name + "_text");
            tagBuilder.SetInnerText("");

            sb.Append(tagBuilder.ToString());
            return new MvcHtmlString(sb.ToString());
        }
    }
}