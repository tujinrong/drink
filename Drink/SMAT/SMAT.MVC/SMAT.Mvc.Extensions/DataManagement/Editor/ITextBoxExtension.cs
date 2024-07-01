using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Collections;
using System.Reflection;
namespace Smat.Mvc.Extensions
{
    public interface ITextBoxExtension
    {
        ITextBoxExtension Name(string name);
        ITextBoxExtension Value(object value);
        ITextBoxExtension Mask(string mask);
        ITextBoxExtension DataType(string dataType);
        ITextBoxExtension Format(string format);
        ITextBoxExtension Pick(bool pick);
        ITextBoxExtension Select(bool select);
        ITextBoxExtension MaxLength(int maxLength);
        ITextBoxExtension Label(string labelText);
        ITextBoxExtension Label(Action<SmatLabel> smatLabel);
        ITextBoxExtension Tooltip(string content);
        ITextBoxExtension Tooltip(Action<SmatTooltip> smatTooltip);
        ITextBoxExtension Events(Action<SmatEvent> smatEvent);
        ITextBoxExtension Enable(bool enable);
        ITextBoxExtension Visible(bool visible);
        ITextBoxExtension HtmlAttributes(object attrs);
        MvcHtmlString ToHtmlString();
        void Render();
    }
}
