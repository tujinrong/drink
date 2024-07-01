using System;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Smat.Mvc.Extensions.Navigation
{
    class RadioButtonExtension : NavigationExtension, IRadioButtonExtension
    {
        private String TagAttribute { get; set; }
        private String ContentAttribute { get; set; }
        private string DataSourceAttribute { get; set; }
        private IList<object> DataSourceObjAttribute { get; set; }
        private String ValueAttribute { get; set; }

        internal RadioButtonExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
        }

        public IRadioButtonExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IRadioButtonExtension Value(string value)
        {
            this.ValueAttribute = value;
            return this;
        }

        public IRadioButtonExtension Label(string labelText) {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }

        public IRadioButtonExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
            return this;
        }

        public IRadioButtonExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public IRadioButtonExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public IRadioButtonExtension DataSource(string dataSource)
        {
            this.DataSourceAttribute = dataSource;
            return this;
        }

        public IRadioButtonExtension DataSource(IList<object> dataSource)
        {
            this.DataSourceObjAttribute = dataSource;
            return this;
        }

        public IRadioButtonExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public IRadioButtonExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IRadioButtonExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public IRadioButtonExtension HtmlAttributes(object attrs)
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
                input = new TagBuilder("input");
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
            input.SetInnerText(this.ContentAttribute);
            return input.ToString();
        }

        protected override string CreateScript()
        {
  
            StringBuilder sb = new StringBuilder();

            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatRadioButton({");
            sb.AppendFormat("value:\"{0}\"", this.ValueAttribute);
            if (this.SmatLabel != null) sb.Append(this.SmatLabel);
            if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
            if (!string.IsNullOrEmpty(this.DataSourceAttribute)) sb.AppendFormat(",dataSource:{0}", this.DataSourceAttribute);
            if (this.DataSourceObjAttribute != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                sb.Append(",dataSource: " + serializer.Serialize(this.DataSourceObjAttribute));
            }
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            sb.Append("});");
            return sb.ToString();
        }
    }
}
