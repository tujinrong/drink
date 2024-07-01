using System;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Smat.Mvc.Extensions.Navigation
{
    class ButtonExtension : NavigationExtension, IButtonExtension
    {
        private String TagAttribute { get; set; }
        private String ContentAttribute { get; set; }

        internal ButtonExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
            this.EnableAttribute = true;
        }

        public IButtonExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IButtonExtension Tag(string tag)
        {
            this.TagAttribute = tag;
            return this;
        }

        public IButtonExtension Content(string content)
        {
            this.ContentAttribute = content;
            return this;
        }
        public IButtonExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public IButtonExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IButtonExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public IButtonExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder input;
            if (!string.IsNullOrEmpty(this.TagAttribute))
            {
                input = new TagBuilder(this.TagAttribute);
            }
            else
            {
                input = new TagBuilder("button");
            }

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
            input.InnerHtml = (this.ContentAttribute);
            return input.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatButton({");
            sb.AppendFormat("content:\"{0}\"", this.ContentAttribute);
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            sb.Append("});");
            return sb.ToString();
        }
    }
}
