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
    public interface ITextAreaExtension
    {
        ITextAreaExtension Name(string name);
        ITextAreaExtension Value(object value);
        ITextAreaExtension MaxLength(int maxLength);
        ITextAreaExtension Label(string labelText);
        ITextAreaExtension Label(Action<SmatLabel> smatLabel);
        ITextAreaExtension Tooltip(string content);
        ITextAreaExtension Tooltip(Action<SmatTooltip> smatTooltip);
        ITextAreaExtension Events(Action<SmatEvent> smatEvent);
        ITextAreaExtension Enable(bool enable);
        ITextAreaExtension Visible(bool visible);
        ITextAreaExtension Resizable(bool resizable);
        ITextAreaExtension Rows(int rows);
        ITextAreaExtension Cols(int cols);
        ITextAreaExtension HtmlAttributes(object attrs);
        MvcHtmlString ToHtmlString();

        void Render();
    }
}
