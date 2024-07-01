using System;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Smat.Mvc.Extensions
{
    class CheckBoxExtension : NavigationExtension, ICheckBoxExtension
    {
        private String GroupNameAttribute { get; set; }
        private String TextAttribute { get; set; }

        protected bool? CheckedAttribute { get; set; }

        internal CheckBoxExtension(HtmlHelper helper, string id)
            : base(helper,id)
        {
            this.EnableAttribute = true;
        }

        public ICheckBoxExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public ICheckBoxExtension GroupName(string groupName)
        {
            this.GroupNameAttribute = groupName;
            return this;
        }

        public ICheckBoxExtension Text(string text)
        {
            this.TextAttribute = text;
            return this;
        }

        public ICheckBoxExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public ICheckBoxExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public ICheckBoxExtension Checked(bool check)
        {
            this.CheckedAttribute = check;
            return this;
        }

        public ICheckBoxExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public ICheckBoxExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public ICheckBoxExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public ICheckBoxExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder input;
            input = new TagBuilder("input");

            if (!string.IsNullOrEmpty(this.IdAttribute)) input.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) input.MergeAttribute("name", this.NameAttribute);
            if (!this.VisibleAttribute)
            {
                if (attrs.ContainsKey("style"))
                {
                    attrs[@"style"] = ("" + attrs[@"style"]) + " display:none;";
                }
                else
                {
                   input.MergeAttribute("style", "display:none;"); 
                }
            }
                
            input.MergeAttributes(attrs);
            return input.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatCheckBox({ boxtype:\"checkbox\"");
            if (!string.IsNullOrEmpty(this.GroupNameAttribute)) sb.AppendFormat(",groupName:\"{0}\"", this.GroupNameAttribute);
            if (!string.IsNullOrEmpty(this.TextAttribute)) sb.AppendFormat(",text:\"{0}\"", this.TextAttribute);
            if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
            if (this.CheckedAttribute != null) sb.AppendFormat(",checked: {0}", this.CheckedAttribute.Value.ToString().ToLower());
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            sb.Append("});");
            return sb.ToString();
        }

    }
}
