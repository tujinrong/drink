using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebEvaluation.DAL;
using WebEvaluation.Models;

namespace System.Web.Mvc.Html 
{
    public static class ButtonExtensions
    {

        public static MvcHtmlString ButtonClose(this HtmlHelper helper,string url,string isCloseWin)
        {
            var sb = new StringBuilder();

            string uuid = System.Guid.NewGuid().ToString();
            sb.Append(@"<button class='btn btn-danger btn-sm' uuid='" + uuid + "' type='button'>&nbsp;取&nbsp;&nbsp;消&nbsp;</button>");

            sb.Append("<script type=\"text/javascript\">");


            if (isCloseWin == "true")
            {
                sb.Append("$('button[uuid = " + uuid + "]').bind('click',function(e){window.close();})");
            }
            else
            {
                sb.Append("$('button[uuid = " + uuid + "]').bind('click',function(e){window.location = '" + url + "';})");
            }

              

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString ButtonClose(this HtmlHelper helper)
        {
            return ButtonClose(helper, "/", "false");
        }

        public static MvcHtmlString ButtonClose(this HtmlHelper helper, string isCloseWin)
        {
            return ButtonClose(helper, "/", isCloseWin);
        }

        public static MvcHtmlString ButtonClose(this HtmlHelper helper,string action, object routeValues)
        {
            //string actionStr = helper.Action(action, routeValues).ToHtmlString();
            
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string actionStr = GetUrl(urlHelper, action, routeValues);

            return ButtonClose(helper, actionStr, "false");
        }

        public static MvcHtmlString ButtonAction(this HtmlHelper helper,bool isOpenForm, string text, string action, object routeValues, object htmlAttributes)
        {
            //string actionStr = helper.Action(action, routeValues).ToHtmlString();

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string actionStr = GetUrl(urlHelper, action, routeValues);

            IDictionary<string, object> attrs = ((IDictionary<string, object>) HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var sb = new StringBuilder();
            string uuid = System.Guid.NewGuid().ToString();

            TagBuilder tagBuilder = new TagBuilder("button");
            tagBuilder.MergeAttributes(attrs);
            tagBuilder.MergeAttribute("type", "button");
            tagBuilder.MergeAttribute("uuid", uuid);
            tagBuilder.SetInnerText(text);

            sb.Append(tagBuilder.ToString());

            sb.Append("<script type=\"text/javascript\">");

            if (isOpenForm)
            {
                sb.Append("$('button[uuid = " + uuid + "]').bind('click',function(e){ if(SMAT.Service.checkAction($(this)) == false)return; SMAT.Service.showTempWindow('" + actionStr + "');})");
            }
            else
            {
                sb.Append("$('button[uuid = " + uuid + "]').bind('click',function(e){if(SMAT.Service.checkAction($(this)) == false)return; window.location = '" + actionStr + "';})");
            }

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString OpenWindow(this HtmlHelper helper, string text, string action, string width, string height, object routeValues, object htmlAttributes)
        {
            //string actionStr = helper.Action(action, routeValues).ToHtmlString();

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string actionStr = GetUrl(urlHelper, action, routeValues);

            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var sb = new StringBuilder();
            string uuid = System.Guid.NewGuid().ToString();

            TagBuilder tagBuilder = new TagBuilder("button");
            tagBuilder.MergeAttributes(attrs);
            tagBuilder.MergeAttribute("type", "button");
            tagBuilder.MergeAttribute("uuid", uuid);
            tagBuilder.SetInnerText(text);

            sb.Append(tagBuilder.ToString());

            sb.Append("<script type=\"text/javascript\">");

            sb.Append("$('button[uuid = " + uuid + "]').bind('click',function(e){window.open('" + actionStr + "', 'newwindow', 'width=" + width + "px;height=" + height + "px;scrollbars=no, resizable=no ,toolbar=no, menubar=no,location=no, status=no');})");


            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString ButtonAction(this HtmlHelper helper, bool isOpenForm, string text, string action, object routeValues)
        {
            return ButtonAction(helper, isOpenForm, text, action, routeValues, null);
        }


        public static MvcHtmlString RadioButtonFor(this HtmlHelper helper,string name, object buttonValue, object value, object htmlAttributes) 
        {
            if (buttonValue == value)
            {
                return helper.RadioButton(name, value, true, htmlAttributes);
            }
            else 
            {
                return helper.RadioButton(name, value, false, htmlAttributes);
            }
            
        }

        public static MvcHtmlString CheckBoxFor(this HtmlHelper helper, string name, List<string> buttonValue, object value, object htmlAttributes)
        {

            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            attrs["value"] = value;

            var sb = new StringBuilder();

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(attrs);
            tagBuilder.MergeAttribute("type", "checkbox");
            tagBuilder.MergeAttribute("name", name);

            if (buttonValue != null && buttonValue.Contains(value))
            {
                tagBuilder.MergeAttribute("checked", "checked");
            }

            sb.Append(tagBuilder.ToString());

            return new MvcHtmlString(sb.ToString());
        }

        public static string GetUrl(UrlHelper urlHelper, string action, object routeValues) 
        {
            RouteValueDictionary values = routeValues == null ? new RouteValueDictionary() : new RouteValueDictionary(routeValues);

            Dictionary<string, object> newValueDic = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> pair in values)
            {
                object routeValue = pair.Value;
                if (routeValue is List<string>)
                {
                    List<string> sValues = (List<string>)routeValue;

                    string newValue = "";
                    foreach (string sv in sValues)
                    {
                        newValue += "&" + pair.Key + "=" + sv;
                    }

                    newValueDic[pair.Key] = newValue;
                }
            }

            foreach (KeyValuePair<string, object> pair in newValueDic)
            {
                values.Remove(pair.Key);
            }

            string actionStr = urlHelper.Action(action, values);

            foreach (KeyValuePair<string, object> pair in newValueDic)
            {
                actionStr += pair.Value;
            }

            return actionStr; 
        }
    }
       
   
}