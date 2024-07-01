using System;
using System.Collections;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public interface IDateTimePickerExtension
    {
        IDateTimePickerExtension Name(string name);
        IDateTimePickerExtension Value(object value);
        IDateTimePickerExtension Format(string format);

        IDateTimePickerExtension Label(string labelText);
        IDateTimePickerExtension Label(Action<SmatLabel> smatLabel);

        IDateTimePickerExtension Enable(bool enable);
        IDateTimePickerExtension Visible(bool visible);
        IDateTimePickerExtension HtmlAttributes(object attrs);
        IDateTimePickerExtension Events(Action<SmatEvent> smatEvent);

        IDateTimePickerExtension Max(string max);
        IDateTimePickerExtension Min(string min);
        IDateTimePickerExtension Interval(int interval);

        


        MvcHtmlString ToHtmlString();
        void Render();
    }
}
