using System;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Smat.Mvc.Extensions.Editor
{
    class DropDownListExtension : EditorExtension, IDropDownListExtension
    {
        private IList ItemsAttribute { get; set; }
        private string CodeKindAttribute { get; set; }
        private string EmptyTextAttribute { get; set; }
        private string DataTypeAttribute { get; set; }
        private string UrlAttribute { get; set; }


        protected bool isComboBoxAttribute { get; set; }

        internal DropDownListExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
        }

        public IDropDownListExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IDropDownListExtension Value(string value)
        {
            this.ValueAttribute = value;
            return this;
        }

        public IDropDownListExtension DataTextField(string dataTextField)
        {
            this.DataTextFieldAttribute = dataTextField;
            return this;
        }

        public IDropDownListExtension DataValueField(string dataValueField)
        {
            this.DataValueFieldAttribute = dataValueField;
            return this;
        }

        public IDropDownListExtension BindTo(IList items)
        {
            this.ItemsAttribute = items;
            return this;
        }

        public IDropDownListExtension CodeKind(string codeKind)
        {
            this.CodeKindAttribute = codeKind;
            return this;
        }

        public IDropDownListExtension EmptyText(string emptyText)
        {
            this.EmptyTextAttribute = emptyText;
            return this;
        }
        public IDropDownListExtension Label(string labelText)
        {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }
        public IDropDownListExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public IDropDownListExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
            return this;
        }

        public IDropDownListExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public IDropDownListExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public IDropDownListExtension DataType(string dataType)
        {
            this.DataTypeAttribute = dataType;
            return this;
        }
        public IDropDownListExtension Url(string url)
        {
            this.UrlAttribute = url;
            return this;
        }

        public IDropDownListExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IDropDownListExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public IDropDownListExtension IsComboBox(bool isComboBox)
        {
            this.isComboBoxAttribute = isComboBox;
            return this;
        }

        public IDropDownListExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }
        protected override string CreateHtml()
        {
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) input.MergeAttribute("name", this.NameAttribute);
            input.MergeAttributes(attrs);
            return input.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatDropDownList({");
            sb.AppendFormat("value:\"{0}\"", this.ValueAttribute);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            if (!this.VisibleAttribute) sb.Append(",visible:false");
            if (this.isComboBoxAttribute) sb.Append(",uitype:\"asmatComboBox\"");
            if (this.SmatLabel != null) sb.Append(this.SmatLabel);
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
            if (!string.IsNullOrEmpty(this.DataTextFieldAttribute)) sb.AppendFormat(",dataTextField:\"{0}\"", this.DataTextFieldAttribute);
            if (!string.IsNullOrEmpty(this.DataValueFieldAttribute)) sb.AppendFormat(",dataValueField:\"{0}\"", this.DataValueFieldAttribute);
            if (!string.IsNullOrEmpty(this.CodeKindAttribute)) sb.AppendFormat(",codeKind:\"{0}\"", this.CodeKindAttribute);
            if (!string.IsNullOrEmpty(this.EmptyTextAttribute)) sb.AppendFormat(",emptyText:\"{0}\"", this.EmptyTextAttribute);
            if (this.ItemsAttribute != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                sb.Append(",dataSource: " + serializer.Serialize(this.ItemsAttribute));
            }

            if (!string.IsNullOrEmpty(this.DataTypeAttribute) && !string.IsNullOrEmpty(this.UrlAttribute))
            {
                sb.Append(",dataSource: {transport: { read: {");
                sb.AppendFormat("dataType:\"{0}\"", this.DataTypeAttribute);
                sb.AppendFormat(",url:\"{0}\"", this.UrlAttribute);
                sb.Append("}}}");
            }

            sb.Append("});");
            return sb.ToString();
        }
    }
}
