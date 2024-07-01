using System;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Smat.Mvc.Extensions.Editor
{
    class DatePickerExtension : EditorExtension, IDatePickerExtension
    {

        private CalendarView StartAttribute { get; set; }
        private CalendarView DepthAttribute { get; set; }
        private String FormatAttribute { get; set; }
        private String MaxAttribute { get; set; }
        private String MinAttribute { get; set; }
        
        internal DatePickerExtension(HtmlHelper helper,string id)
            : base(helper,id)
        {
        }

        public IDatePickerExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IDatePickerExtension Value(object value)
        {
            if (value is DateTime)
            {
                this.ValueAttribute = ((DateTime)value).ToString("yyyy/MM/dd");
            }
            else if (value == null)
            {
                this.ValueAttribute = "";
            }
            else
            {
                this.ValueAttribute = value.ToString();
            }
            return this;
        }

        public IDatePickerExtension Label(string labelText)
        {
            this.SmatLabel = new SmatLabel();
            this.SmatLabel.Text(labelText);
            return this;
        }

        public IDatePickerExtension Label(Action<SmatLabel> smatLabel)
        {
            this.SmatLabel = new SmatLabel();
            smatLabel(this.SmatLabel);
            return this;
        }

        public IDatePickerExtension Events(Action<SmatEvent> smatEvent)
        {
            this.SmatEvent = new SmatEvent();
            smatEvent(this.SmatEvent);
            return this;
        }

        public IDatePickerExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public IDatePickerExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

        public IDatePickerExtension Start(CalendarView view)
        {
            this.StartAttribute = view;
            return this;
        }

        public IDatePickerExtension Depth(CalendarView view)
        {
            this.DepthAttribute = view;
            return this;
        }

        public IDatePickerExtension Format(string format)
        {
            this.FormatAttribute = format;
            return this;
        }
        public IDatePickerExtension Max(string max)
        {
            this.MaxAttribute = max;
            return this;
        }
        public IDatePickerExtension Min(string min)
        {
            this.MinAttribute = min;
            return this;
        }

        public IDatePickerExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IDatePickerExtension Visible(bool visible)
        {
            this.VisibleAttribute = visible;
            return this;
        }

        public IDatePickerExtension HtmlAttributes(object attrs)
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
            sb.Append("\").smatDatePicker({");
            sb.AppendFormat("value:\"{0}\"", this.ValueAttribute);
            sb.AppendFormat(",start:\"{0}\"", this.GetCalendarViewString(this.StartAttribute));
            sb.AppendFormat(",depth:\"{0}\"", this.GetCalendarViewString(this.DepthAttribute));
            if (this.SmatLabel != null) sb.Append(this.SmatLabel);
            if (this.SmatEvent != null) sb.Append(this.SmatEvent);
            if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
            if (!string.IsNullOrEmpty(this.FormatAttribute)) sb.AppendFormat(",format:\"{0}\"", this.FormatAttribute);
            if (!string.IsNullOrEmpty(this.MaxAttribute)) sb.AppendFormat(",max:new Date(\"{0}\")", this.MaxAttribute);
            if (!string.IsNullOrEmpty(this.MinAttribute)) sb.AppendFormat(",min:new Date(\"{0}\")", this.MinAttribute);
            if (!this.EnableAttribute) sb.Append(",enable:false");
            if (!this.VisibleAttribute) sb.Append(",visible:false");
            if (this.HeightAttribute != 200) sb.AppendFormat(",height:{0}", this.HeightAttribute);
            if (this.MinLengthAttribute != 1) sb.AppendFormat(",minLength:{0}", this.MinLengthAttribute);
            sb.Append("});");
            return sb.ToString();
        }

        private string GetCalendarViewString(CalendarView view)
        {
            switch (view)
            { 
                case CalendarView.Month:
                    return "month";
                case CalendarView.Year:
                    return "year";
                case CalendarView.Decade:
                    return "decade";
                case CalendarView.Century:
                    return "century";
                default:
                    return "month";
            }
        }
    }
}
