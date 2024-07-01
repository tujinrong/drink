using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions.Navigation
{
    public interface IRadioButtonExtension
    {
        IRadioButtonExtension Name(string name);
        IRadioButtonExtension Value(string value);
        IRadioButtonExtension Enable(bool enable);
        IRadioButtonExtension Visible(bool visible);
        IRadioButtonExtension Label(string labelText);
        IRadioButtonExtension Label(Action<SmatLabel> smatLabel);

        IRadioButtonExtension Tooltip(string content);
        IRadioButtonExtension Tooltip(Action<SmatTooltip> smatTooltip);
        IRadioButtonExtension Events(Action<SmatEvent> smatEvent);
        IRadioButtonExtension DataSource(string dataSource);
        IRadioButtonExtension DataSource(IList<object> dataSource);
        IRadioButtonExtension HtmlAttributes(object attrs);
        MvcHtmlString ToHtmlString();
        void Render();
    }
}
