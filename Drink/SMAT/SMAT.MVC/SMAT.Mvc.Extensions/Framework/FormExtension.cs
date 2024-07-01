using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Smat.Mvc.Extensions.Framework
{
    public static class FormExtension
    {
        public static SmatMvcForm BeginForm(this HtmlHelper helper, string name, string reset, object htmlAttributes)
        {
            return BeginForm(helper, name, reset, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static SmatMvcForm BeginForm(this HtmlHelper helper, string name, string reset, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("id", name);

            helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine("  jQuery(document).ready(function () {");
            sb.Append("jQuery(\"#");
            sb.Append(name);
            sb.Append("\").smatForm({");
            sb.Append("actions:[#actions#]");
            sb.Append("});");
            sb.AppendLine("  });");
            sb.AppendLine("</script>");

            SmatMvcForm result = new SmatMvcForm(helper.ViewContext);
            result.SetScript(sb.ToString());

            return result;
        }
    }
}
