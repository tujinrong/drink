using SMAT.MVC.SMAT.Mvc.Extensions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Smat.Mvc.Extensions.Editor
{
    class TextBoxExtension : EditorExtension, ITextBoxExtension
    {
        private string MaskAttribute { get; set; }
        private string DataTypeAttribute { get; set; }
        private string FormatAttribute { get; set; }
        private bool? PickAttribute { get; set; }
        private bool? SelectAttribute { get; set; }
        internal TextBoxExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
            this.EnableAttribute = true;
        }

        public ITextBoxExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public ITextBoxExtension Value(object value)
        {
            if (value != null)
            {
                this.ValueAttribute = value.ToString();
            }
            return this;
        }

        public ITextBoxExtension Mask(string mask)
        {
            this.MaskAttribute = mask;
            return this;
        }
        public ITextBoxExtension DataType(string dataType)
        {
            this.DataTypeAttribute = dataType;
            return this;
        }
        public ITextBoxExtension Format(string format)
        {
            this.FormatAttribute = format;
            return this;
        }
        public ITextBoxExtension Pick(bool pick)
        {
            this.PickAttribute = pick;
            return this;
        }
        public ITextBoxExtension Select(bool select)
        {
            this.SelectAttribute = select;
            return this;
        }
        public ITextBoxExtension MaxLength(int maxLength)
        {
            this.MaxLengthAttribute = maxLength;
            return this;
        }

        public ITextBoxExtension Label(string labelText)
        {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }

        public ITextBoxExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
            return this;
        }
        public ITextBoxExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public ITextBoxExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public ITextBoxExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public ITextBoxExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public ITextBoxExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public ITextBoxExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder input = new TagBuilder("input");
            if (!string.IsNullOrEmpty(this.IdAttribute)) input.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) input.MergeAttribute("name", this.NameAttribute);
            if (this.MaxLengthAttribute != 0) input.MergeAttribute("maxlength", this.MaxLengthAttribute.ToString());
            input.MergeAttributes(attrs);
            return input.ToString();  
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(DataTypeAttribute) && DataTypeAttribute == "onlyNum")
            {
                sb.Append("jQuery(\"#");
                sb.Append(this.IdAttribute);
                sb.Append("\").smatNumericTextBox({");
                sb.AppendFormat("value:\"{0}\"", MvcUtil.CAttrStr(this.ValueAttribute));
                if (this.SmatLabel != null) sb.Append(this.SmatLabel);
                if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
                if (this.SmatEvent != null) sb.Append(this.SmatEvent);
                if (!string.IsNullOrEmpty(this.FormatAttribute)) sb.AppendFormat(",format:\"{0}\"", this.FormatAttribute);
                if (this.PickAttribute != null) sb.AppendFormat(",pick:{0}", this.PickAttribute.Value ? "true" : "false");
                if (this.SelectAttribute != null) sb.AppendFormat(",select:{0}", this.SelectAttribute.Value ? "true" : "false");
                if (!this.EnableAttribute) sb.Append(",enable:false");
                if (!this.VisibleAttribute) sb.Append(",visible:false");
                if (this.HeightAttribute != 200) sb.AppendFormat(",height:{0}", this.HeightAttribute);
                if (this.MinLengthAttribute != 1) sb.AppendFormat(",minLength:{0}", this.MinLengthAttribute);
                sb.Append("});");
            }
            else
            {
                sb.Append("jQuery(\"#");
                sb.Append(this.IdAttribute);
                sb.Append("\").smatTextBox({");
                sb.AppendFormat("value:\"{0}\"", MvcUtil.CAttrStr(this.ValueAttribute));
                if (this.SmatLabel != null) sb.Append(this.SmatLabel);
                if (this.SmatEvent != null) sb.Append(this.SmatEvent);
                if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
                if (!string.IsNullOrEmpty(this.MaskAttribute)) sb.AppendFormat(",mask:\"{0}\"", this.MaskAttribute);
                if (!string.IsNullOrEmpty(this.DataTypeAttribute)) sb.AppendFormat(",dataType:\"{0}\"", this.DataTypeAttribute);
                if (!this.EnableAttribute) sb.Append(",enable:false");
                if (!this.VisibleAttribute) sb.Append(",visible:false");
                if (this.HeightAttribute != 200) sb.AppendFormat(",height:{0}", this.HeightAttribute);
                if (this.MinLengthAttribute != 1) sb.AppendFormat(",minLength:{0}", this.MinLengthAttribute);
                sb.Append("});");
            }
            return sb.ToString();
        }
    }
}
