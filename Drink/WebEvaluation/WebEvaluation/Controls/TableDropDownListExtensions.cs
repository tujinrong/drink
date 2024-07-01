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

    public static class TableDropDownListExtensions
    {
        /// <summary>
        ///M_グループ のデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString GroupDropDownList(this HtmlHelper helper, string name, object htmlAttributes)
        {
            EvaluationContext db = new EvaluationContext();

            List<M_Group> groups = db.Groups.ToList<M_Group>();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "　　　　　　　　　", Value = "" });
            foreach (M_Group g in groups)
            {
                list.Add(new SelectListItem { Text = g.GroupName, Value = g.GroupCD });
            }


            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            if (attrs.ContainsKey(@"class"))
            {
                if (("" + attrs[@"class"]).Contains("input-sm") == false)
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " input-sm";
                }
            }
            else 
            {
                attrs[@"class"] = "input-sm";
            }

            return helper.DropDownList(name, list, attrs);
        }

        public static MvcHtmlString GroupDropDownList(this HtmlHelper helper, string name,string id,string value)
        {
            var sb = new StringBuilder();

            string uuid = System.Guid.NewGuid().ToString();

            //sb.Append(@"<select uuid='" + uuid + "'  class='populate select2-offscreen form-control input-sm'>");

            string idStr = "";
            if (id != null) 
            {
                idStr = "id = '" + id + "'";
            }

            sb.Append(@"<select " + idStr + " name='" + name + "' id='" + name + "' uuid='" + uuid + "'  class='form-control input-sm'>");
            
            sb.AppendLine("<option></option>");

            EvaluationContext db = new EvaluationContext();

            var quare =
                from g in db.Groups
                join d in db.Divisions on g.DivCD equals d.DivCD into g_d
                from div in g_d.DefaultIfEmpty()
                group g by new { div.DivCD, div.DivName } into result
                select result;

            foreach (var gp in quare)
            {
                if (gp.Key.DivCD == value)
                {
                    sb.AppendLine("<option value='" + gp.Key.DivCD + "' selected='selected'>" + gp.Key.DivName + "</option>");
                }
                else
                {
                    sb.AppendLine("<option value='" + gp.Key.DivCD + "'>" + gp.Key.DivName + "</option>");
                }
               
                foreach (var item in gp)
                {
                    if ("g_" + item.GroupCD == value)
                    {
                        sb.AppendLine("<option  value='g_" + item.GroupCD + "' selected='selected'>　" + item.GroupName + "</option>");
                    }
                    else 
                    {
                        sb.AppendLine("<option  value='g_" + item.GroupCD + "'>　" + item.GroupName + "</option>");
                    }
                }
            }

            //e.g
            //sb.AppendLine("<option>東日本</option>");
            // sb.AppendLine("<option>・1G</option>");
            // sb.AppendLine("<option>・2G</option>");
            // sb.AppendLine("<option>・3G</option>");
            // sb.AppendLine("<option>関東</option>");
            // sb.AppendLine("<option>・4G</option>");
            // sb.AppendLine("<option>・5G</option>");
            // sb.AppendLine("<option>・6G</option>");
            // sb.AppendLine("<option>・PD</option>");
            // sb.AppendLine("<option>東海</option>");
            // sb.AppendLine("<option>・7G</option>");
            // sb.AppendLine("<option>・8G</option>");
            // sb.AppendLine("<option>関西</option>");
            // sb.AppendLine("<option>・9G</option>");
            // sb.AppendLine("<option>・10G</option>");
            // sb.AppendLine("<option>西日本</option>");
            // sb.AppendLine("<option>・11G</option>");
            // sb.AppendLine("<option>・12G</option>");
                    
             sb.AppendLine("</select>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString GroupDropDownList(this HtmlHelper helper, string name,string value)
        {
            return GroupDropDownList(helper,name,null,value);
        }

        /// <summary>
        /// M_事業部のデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString DivisionDropDownList(this HtmlHelper helper, string name, object htmlAttributes)
        {
            EvaluationContext db = new EvaluationContext();

            List<M_Division> allItems = db.Divisions.ToList<M_Division>();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "　　　　　　　　　", Value = "" });
            foreach (M_Division item in allItems)
            {
                list.Add(new SelectListItem { Text = item.DivName, Value = item.DivCD });
            }


            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            if (attrs.ContainsKey(@"class"))
            {
                if (("" + attrs[@"class"]).Contains("input-sm") == false) 
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " input-sm";
                }
            }
            else
            {
                attrs[@"class"] = "input-sm";
            }

            return helper.DropDownList(name, list, attrs);
        }

        public static MvcHtmlString DivisionDropDownList(this HtmlHelper helper, string name)
        {
            return DivisionDropDownList(helper, name, null);
        }

        /// <summary>
        /// S_コードのデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString CodeDropDownList(this HtmlHelper helper, string name, string Kind,string value, object htmlAttributes)
        {
            EvaluationContext db = new EvaluationContext();

           
            var allItems = from s in db.Codes
                           where s.Kind == Kind
                           orderby s.Kind , s.CD
                         select s;

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "　　　　　　　　　", Value = "" });
            foreach (var item in allItems)
            {
                if (item.CD.Trim() == value)
                {
                    list.Add(new SelectListItem { Text = item.Name, Value = item.CD.Trim(),Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem { Text = item.Name, Value = item.CD.Trim() });
            }
            }


            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            if (attrs.ContainsKey(@"class"))
            {
                if (("" + attrs[@"class"]).Contains("input-sm") == false)
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " input-sm";
                }
            }
            else
            {
                attrs[@"class"] = "input-sm";
            }

            return helper.DropDownList(name, list, attrs);
        }

        public static MvcHtmlString CodeDropDownList(this HtmlHelper helper, string name, string Kind)
        {
            return CodeDropDownList(helper, name,Kind,null, null);
        }

        /// <summary>
        /// S_コードのデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString CodeDropDownListOrderByName(this HtmlHelper helper, string name, string Kind, string value, object htmlAttributes)
        {
            EvaluationContext db = new EvaluationContext();


            var allItems = from s in db.Codes

                           where s.Kind == Kind

                           orderby s.Name

                           select s;

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "　　　　　　　　　", Value = "" });
            foreach (var item in allItems)
            {
                if (item.CD == value)
                {
                    list.Add(new SelectListItem { Text = item.CD, Value = item.CD, Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem { Text = item.CD, Value = item.CD });
                }
            }


            IDictionary<string, object> attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            if (attrs.ContainsKey(@"class"))
            {
                if (("" + attrs[@"class"]).Contains("input-sm") == false)
                {
                    attrs[@"class"] = ("" + attrs[@"class"]) + " input-sm";
                }
            }
            else
            {
                attrs[@"class"] = "input-sm";
            }

            return helper.DropDownList(name, list, attrs);
        }

        public static MvcHtmlString CodeDropDownListOrderByName(this HtmlHelper helper, string name, string Kind)
        {
            return CodeDropDownListOrderByName(helper, name, Kind, null, null);
        }

        /// <summary>
        ///組織マスタ のデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString UnitDropDownList(this HtmlHelper helper, string name, string value, object htmlAttributes)
        {
            EvaluationContext db = new EvaluationContext();

            List<S_Unit> units = db.Units.ToList<S_Unit>();

            var sb = new StringBuilder();

            sb.Append(@"<select  name='" + name + "' id='" + name + "' class='form-control input-sm'>");
            sb.AppendLine("<option></option>");
            foreach (S_Unit u in units)
            {

                if (u.UnitCD == value)
                {
                    sb.AppendLine("<option value='" + u.UnitCD + "' selected='selected'>" + u.UnitName + "</option>");
                }
                else
                {
                    sb.AppendLine("<option value='" + u.UnitCD + "'>" + u.UnitName + "</option>");
                }
            }
            sb.Append(@"</select>");

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        ///組織マスタ のデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString UnitDropDownList(this HtmlHelper helper, string name, string value)
        {
            return UnitDropDownList(helper, name, value, null);
        }

        /// <summary>
        ///組織マスタ のデータDropDownListで表示
        /// </summary>
        /// <param name="helper">htmlヘルパー</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static MvcHtmlString UnitDropDownList(this HtmlHelper helper, string name)
        {
            return UnitDropDownList(helper, name, null, null);
        }

        public static MvcHtmlString UnitDropDownList<TModel, TValue>(this HtmlHelper<TModel> helper, string name, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {

            string valueStr = helper.DisplayFor(expression).ToString();

            return UnitDropDownList(helper, name, valueStr, htmlAttributes);
        }

    }
}