using System;
using System.Collections;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public interface ITimePickerExtension
    {
        ITimePickerExtension Name(string name);
        ITimePickerExtension Value(object value);
        ITimePickerExtension Format(string format);

        ITimePickerExtension Label(string labelText);
        ITimePickerExtension Label(Action<SmatLabel> smatLabel);

        ITimePickerExtension Enable(bool enable);
        ITimePickerExtension Visible(bool visible);
        ITimePickerExtension HtmlAttributes(object attrs);
        ITimePickerExtension Events(Action<SmatEvent> smatEvent);

        ITimePickerExtension Max(string max);
        ITimePickerExtension Min(string min);


        MvcHtmlString ToHtmlString();
        void Render();
    }
}
