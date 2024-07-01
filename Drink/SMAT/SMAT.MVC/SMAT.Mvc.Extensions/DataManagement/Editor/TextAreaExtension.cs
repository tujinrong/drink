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
    class TextAreaExtension : EditorExtension, ITextAreaExtension
    {
        private bool ResizableAttribute { get; set; }

        private int? RowsAttribute { get; set; }

        private int? ColsAttribute { get; set; }
        internal TextAreaExtension(HtmlHelper helper, string id)
            : base(helper,id)
        {
            this.EnableAttribute = true;
            this.ResizableAttribute = true;
            this.RowsAttribute = 0;
            this.ColsAttribute = 0;
        }

        public ITextAreaExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public ITextAreaExtension Value(object value)
        {
            if (value != null)
            {
                this.ValueAttribute = value.ToString();
            }
            return this;
        }

        public ITextAreaExtension Resizable(bool resizable)
        {
            this.ResizableAttribute = resizable;
            return this;
        }

        public ITextAreaExtension Rows(int rows)
        {
            this.RowsAttribute = rows;
            return this;
        }

        public ITextAreaExtension Cols(int cols)
        {
            this.ColsAttribute = cols;
            return this;
        }
        
        public ITextAreaExtension MaxLength(int maxLength)
        {
            this.MaxLengthAttribute = maxLength;
            return this;
        }

        public ITextAreaExtension Label(string labelText)
        {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }

        public ITextAreaExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
            return this;
        }
        public ITextAreaExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public ITextAreaExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public ITextAreaExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public ITextAreaExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public ITextAreaExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public ITextAreaExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder input = new TagBuilder("textarea");
            if (!string.IsNullOrEmpty(this.IdAttribute)) input.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) input.MergeAttribute("name", this.NameAttribute);
            if (this.MaxLengthAttribute != 0) input.MergeAttribute("maxlength", this.MaxLengthAttribute.ToString());


            if (!string.IsNullOrEmpty(this.ValueAttribute)) { } input.InnerHtml = this.ValueAttribute;

            input.MergeAttributes(attrs);
            return input.ToString();  
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            {
                sb.Append("jQuery(\"#");
                sb.Append(this.IdAttribute);
                sb.Append("\").smatTextArea({");
                sb.AppendFormat("temp:\"\"");
                if (this.SmatLabel != null) sb.Append(this.SmatLabel);
                if (this.SmatEvent != null) sb.Append(this.SmatEvent);
                if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
                if (!this.EnableAttribute) sb.Append(",enable:false");
                if (this.RowsAttribute > 0) sb.AppendFormat(",rows:{0}", this.RowsAttribute);
                if (this.ColsAttribute > 0) sb.AppendFormat(",cols:{0}", this.ColsAttribute);
                if (!this.ResizableAttribute) sb.Append(",resize:\"none\"");
                if (!this.VisibleAttribute) sb.Append(",visible:false");
                if (this.HeightAttribute != 200) sb.AppendFormat(",height:{0}", this.HeightAttribute);
                if (this.MinLengthAttribute != 1) sb.AppendFormat(",minLength:{0}", this.MinLengthAttribute);
                sb.Append("});");
            }
            return sb.ToString();
        }
    }
}
