using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public interface ICheckBoxExtension
    {
        ICheckBoxExtension Name(string name);
        ICheckBoxExtension GroupName(string groupName);
        ICheckBoxExtension Text(string text);
        ICheckBoxExtension Enable(bool enable);
        ICheckBoxExtension Checked(bool check);
        ICheckBoxExtension Visible(bool visible);
        ICheckBoxExtension Events(Action<SmatEvent> smatEvent);
        ICheckBoxExtension HtmlAttributes(object attrs);

        ICheckBoxExtension Tooltip(string content);
        ICheckBoxExtension Tooltip(Action<SmatTooltip> smatTooltip);

        MvcHtmlString ToHtmlString();
        void Render();
    }
}
