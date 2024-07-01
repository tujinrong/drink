using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.DAL;
using WebEvaluation.Models;

namespace System.Web.Mvc.Html
{
    public static class PickerExtensions
    {
        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name, object value)
        {
            return DatePicker(helper, name, value, null);
        }

        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name, object value, object htmlAttributes)
        {
            var sb = new StringBuilder();

            string valueStr = "";
            if (value != null)
            {
                if (value is DateTime)
                {
                    DateTime date = (DateTime)value;
                    valueStr = date.ToString("yyyy/MM/dd");
                }
                else 
                {
                    valueStr = value.ToString();
                }
                
            }

            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            if (attrs.ContainsKey(@"class"))
            {
                if (("" + attrs[@"class"]).Contains("form-control") == false)
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " form-control";
                }

                if (("" + attrs[@"class"]).Contains("input-sm") == false)
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " input-sm";
                }
            }
            else
            {
                attrs[@"class"] = "form-control input-sm"; 
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(attrs);
            tagBuilder.MergeAttribute("type", "text");
            tagBuilder.MergeAttribute("id", name);
            tagBuilder.MergeAttribute("name", name);
            tagBuilder.MergeAttribute("value", valueStr);
            //tagBuilder.SetInnerText(valueStr);

            string uuid = System.Guid.NewGuid().ToString();
            sb.Append(@"<div class='input-group date' uuid='" + uuid + "'>" + tagBuilder.ToString() + "<span class='input-group-addon input-sm'><i class='glyphicon glyphicon-time'></i></span></div>");

            sb.Append("<script type=\"text/javascript\">");

            sb.Append("$('div[uuid = " + uuid + "]').datepicker({format: 'yyyy/mm/dd',language: 'ja',todayBtn:'linked',todayHighlight:true,autoclose:true});");

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString DatePicker(this HtmlHelper helper, string name)
        {
            return DatePicker(helper, name, null);
        }

        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string name, object value)
        {
            var sb = new StringBuilder();

            string valueStr = "";
            if (value != null)
            {
                if (value is DateTime)
                {
                    DateTime date = (DateTime)value;
                    valueStr = " value = '" + date.ToString("yyyy/MM/dd HH:mm") + "' ";
                }
                else
                {
                    valueStr = " value = '" + value + "' ";
                }

            }

            string uuid = System.Guid.NewGuid().ToString();
            sb.Append(@"<div class='input-group date' uuid='" + uuid + "'><input type='text' id='" + name.Replace(".","_") + "' name='" + name + "' " + valueStr + " class='form-control input-sm isCheckChange'><span class='input-group-addon input-sm'><i class='glyphicon glyphicon-time'></i></span></div>");

            sb.Append("<script type=\"text/javascript\">");

            sb.Append("$('div[uuid = " + uuid + "]').datetimepicker({format: 'yyyy/mm/dd hh:ii',language: 'ja',todayBtn:'linked',startDate:'2000-01-01',todayHighlight:true,autoclose:true});");

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string name)
        {
            return DateTimePicker(helper, name, null);
        }

        public static MvcHtmlString TimePicker(this HtmlHelper helper, string name, object value)
        {
            var sb = new StringBuilder();

            string valueStr = "";
            if (value != null)
            {
                {
                    valueStr = " value = '" + value + "' ";
                }

            }

            string uuid = System.Guid.NewGuid().ToString();
            sb.Append(@"<div class='input-group date' uuid='" + uuid + "'><input type='text' name='" + name + "' " + valueStr + " class='form-control input-sm'><span class='input-group-addon input-sm'><i class='glyphicon glyphicon-time'></i></span></div>");

            sb.Append("<script type=\"text/javascript\">");

            sb.Append("$('div[uuid = " + uuid + "]').datetimepicker({format: 'hh:ii',language: 'ja',todayBtn:'linked',maxView: 1,startView:1,todayHighlight:true,autoclose:true});");

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString TimePicker(this HtmlHelper helper, string name)
        {
            return TimePicker(helper, name, null);
        }

        public static MvcHtmlString MonthsPicker(this HtmlHelper helper, string name, object value)
        {
            var sb = new StringBuilder();

            string valueStr = "";
            if (value != null)
            {
                valueStr = " value = '" + value + "' ";
            }

            string uuid = System.Guid.NewGuid().ToString();
            sb.Append(@"<div class='input-group date' uuid='" + uuid + "'><input type='text' name='" + name + "' " + valueStr + " class='form-control input-sm'><span class='input-group-addon input-sm'><i class='glyphicon glyphicon-time'></i></span></div>");

            sb.Append("<script type=\"text/javascript\">");

            sb.Append("$('div[uuid = " + uuid + "]').datepicker({format: 'yyyy/mm',language: 'ja',todayBtn:'linked',viewMode: 'months',minViewMode: 'months',todayHighlight:true,autoclose:true});");

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString MonthsPicker(this HtmlHelper helper, string name)
        {
            return MonthsPicker(helper, name, null);
        }

        public static MvcHtmlString YearsPicker(this HtmlHelper helper, string name, object value)
        {
            var sb = new StringBuilder();

            string valueStr = "";
            if (value != null)
            {
                valueStr = " value = '" + value + "' ";
            }

            string uuid = System.Guid.NewGuid().ToString();
            sb.Append(@"<div class='input-group date' uuid='" + uuid + "'><input type='text' name='" + name + "' " + valueStr + " class='form-control input-sm'><span class='input-group-addon input-sm'><i class='glyphicon glyphicon-time'></i></span></div>");

            sb.Append("<script type=\"text/javascript\">");

            sb.Append("$('div[uuid = " + uuid + "]').datepicker({format: 'yyyy',language: 'ja',todayBtn:'linked',viewMode: 'years',minViewMode: 'years',todayHighlight:true,autoclose:true});");

            sb.Append("</script>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString YearsPicker(this HtmlHelper helper, string name)
        {
            return YearsPicker(helper, name, null);
        }
    }
}