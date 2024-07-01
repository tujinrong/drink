using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions.Navigation
{
    public interface IButtonExtension
    {
        IButtonExtension Name(string name);
        IButtonExtension Tag(string tag);
        IButtonExtension Enable(bool enable);
        IButtonExtension Visible(bool visible);
        IButtonExtension Content(string content);
        IButtonExtension Events(Action<SmatEvent> smatEvent);
        IButtonExtension HtmlAttributes(object attrs);
        MvcHtmlString ToHtmlString();
        void Render();
    }
}
