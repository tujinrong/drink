using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    class ReferExtension : EditorExtension, IReferExtension
    {
        private String ReferKeyAttribute { get; set; }
        private String TextAttribute { get; set; }
        private String ValueFieldAttribute { get; set; }
        private String DisplayFieldAttribute { get; set; }
        private IDictionary<string, object> ObjParameters { get; set; }
        private string Parameters { get; set; }
        private string AfterSetValueFuncAttribute { get; set; }

        internal ReferExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
        }

        public IReferExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }
        public IReferExtension Value(string value)
        {
            this.ValueAttribute = value;
            return this;
        }
        public IReferExtension Text(string text)
        {
            this.TextAttribute = text;
            return this;
        }

        public IReferExtension Label(string labelText)
        {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }

        public IReferExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
            return this;
        }

        public IReferExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public IReferExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public IReferExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public IReferExtension ReferKey(string referKey)
        {
            this.ReferKeyAttribute = referKey;
            return this;
        }

        public IReferExtension ValueField(string valueField)
        {
            this.ValueFieldAttribute = valueField;
            return this;
        }

        public IReferExtension DisplayField(string displayField)
        {
            this.DisplayFieldAttribute = displayField;
            return this;
        }

        public IReferExtension Param(object param)
        {
            this.ObjParameters = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(param)); ;
            return this;
        }

        public IReferExtension GetParam(string param)
        {
            this.Parameters = param;
            return this;
        }
        public IReferExtension AfterSetValue(string afterSetValueFunc)
        {
            this.AfterSetValueFuncAttribute = afterSetValueFunc;
            return this;
        }
        public IReferExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IReferExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public IReferExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) input.MergeAttribute("name", this.NameAttribute);
            input.MergeAttribute("class", "s-input s-refer");
            input.MergeAttribute("refer-key", this.ReferKeyAttribute);
            if (!string.IsNullOrEmpty(this.ValueFieldAttribute)) input.MergeAttribute("value-field", this.ValueFieldAttribute);
            if (!string.IsNullOrEmpty(this.DisplayFieldAttribute)) input.MergeAttribute("display-field", this.DisplayFieldAttribute);
            input.MergeAttributes(attrs);
            return input.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatRefer({");
            sb.AppendFormat("value:\"{0}\"", this.ValueAttribute);
            if (!string.IsNullOrEmpty(this.TextAttribute)) sb.AppendFormat(",text:\"{0}\"", this.TextAttribute);
            if (this.SmatLabel != null) sb.Append(this.SmatLabel);
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (!string.IsNullOrEmpty(this.AfterSetValueFuncAttribute)) sb.AppendFormat(",afterSetValue:{0}", this.AfterSetValueFuncAttribute);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            if (!this.VisibleAttribute) sb.Append(",visible:false");
            if (this.ObjParameters != null && this.ObjParameters.Count > 0)
            {
                string paramStr = "";

                foreach (var param in this.ObjParameters)
                {
                    paramStr += (param.Key + ":\"" + param.Value + "\",");
                }

                if (paramStr.EndsWith(",")) paramStr = paramStr.Substring(0, paramStr.Length - 1);

                sb.Append(",getParam: function (e) {");
                sb.Append(" var p = {");
                sb.Append(paramStr);
                sb.Append("};");
                sb.Append("return p;");
                sb.Append("}");
            }
            else
            {
                if (this.Parameters != null && this.Parameters.Length > 0)
                {
                    sb.AppendFormat(",getParam: {0}", this.Parameters);
                }
            }

            sb.Append("});");
            return sb.ToString();
        }
    }
}
