using System;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Smat.Mvc.Extensions.Editor
{
    class DateTimePickerExtension : EditorExtension, IDateTimePickerExtension
    {
        private String FormatAttribute { get; set; }


        private String MaxAttribute { get; set; }
        private String MinAttribute { get; set; }

        private int IntervalAttribute { get; set; }

        
        internal DateTimePickerExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
        }

        public IDateTimePickerExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IDateTimePickerExtension Value(object value)
        {
            if (value is DateTime)
            {
                this.ValueAttribute = ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss");
            }
            else
            {
                this.ValueAttribute = value.ToString();
            }
            return this;
        }

        public IDateTimePickerExtension Format(string format)
        {
            this.FormatAttribute = format;
            return this;
        }

        public IDateTimePickerExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IDateTimePickerExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public IDateTimePickerExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        public IDateTimePickerExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public IDateTimePickerExtension Max(string max)
        {
            this.MaxAttribute = max;
            return this;
        }
        public IDateTimePickerExtension Min(string min)
        {
            this.MinAttribute = min;
            return this;
        }
        public IDateTimePickerExtension Interval(int interval)
        {
            this.IntervalAttribute = interval;
            return this;
        }


        public IDateTimePickerExtension Label(string labelText)
        {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }

        public IDateTimePickerExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
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
            sb.Append("\").smatDateTimePicker({");
            sb.AppendFormat("value:\"{0}\"", this.ValueAttribute);
            if (!string.IsNullOrEmpty(this.FormatAttribute)) sb.AppendFormat(",format:\"{0}\"", this.FormatAttribute);
            if (this.SmatLabel != null) sb.Append(this.SmatLabel);
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (!string.IsNullOrEmpty(this.MaxAttribute)) sb.AppendFormat(",max:new Date(\"{0}\")", this.MaxAttribute);
            if (!string.IsNullOrEmpty(this.MinAttribute)) sb.AppendFormat(",min:new Date(\"{0}\")", this.MinAttribute);
            if (!this.VisibleAttribute) sb.Append(",visible:false");
            if (this.HeightAttribute != 200) sb.AppendFormat(",height:{0}", this.HeightAttribute);
            if (this.MinLengthAttribute != 1) sb.AppendFormat(",minLength:{0}", this.MinLengthAttribute);
            if (this.IntervalAttribute > 0) sb.AppendFormat(",interval:{0}", this.MinLengthAttribute);
            sb.Append("});");
            return sb.ToString();
        }
    }
}
