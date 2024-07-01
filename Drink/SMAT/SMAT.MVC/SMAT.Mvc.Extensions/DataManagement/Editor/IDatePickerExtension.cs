using System;
using System.Collections;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public interface IDatePickerExtension
    {
        IDatePickerExtension Name(string name);
        IDatePickerExtension Value(object value);
        IDatePickerExtension Label(string labelText);
        IDatePickerExtension Label(Action<SmatLabel> smatLabel);
        IDatePickerExtension Tooltip(string content);
        IDatePickerExtension Tooltip(Action<SmatTooltip> smatTooltip);
        IDatePickerExtension Start(CalendarView view);
        IDatePickerExtension Depth(CalendarView view);
        IDatePickerExtension Format(string format);
        IDatePickerExtension Enable(bool enable);
        IDatePickerExtension Visible(bool visible);
        IDatePickerExtension HtmlAttributes(object attrs);

        IDatePickerExtension Max(string max);
        IDatePickerExtension Min(string min);

        IDatePickerExtension Events(Action<SmatEvent> smatEvent);

        MvcHtmlString ToHtmlString();
        void Render();
    }
}
