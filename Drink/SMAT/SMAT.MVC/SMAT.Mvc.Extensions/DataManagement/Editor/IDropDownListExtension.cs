using System;
using System.Collections;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public interface IDropDownListExtension
    {
        IDropDownListExtension Name(string name);
        IDropDownListExtension DataTextField(string dataTextField);
        IDropDownListExtension DataValueField(string dataValueField);
        IDropDownListExtension BindTo(IList items);
        IDropDownListExtension CodeKind(string codeKind);
        IDropDownListExtension EmptyText(string emptyText);
        IDropDownListExtension Label(string labelText);
        IDropDownListExtension Label(Action<SmatLabel> smatLabel);
        IDropDownListExtension Tooltip(string content);
        IDropDownListExtension Tooltip(Action<SmatTooltip> smatTooltip);
        IDropDownListExtension DataType(string dataType);
        IDropDownListExtension Url(string url);
        IDropDownListExtension Value(string value);
        IDropDownListExtension Enable(bool enable);
        IDropDownListExtension Visible(bool visible);
        IDropDownListExtension HtmlAttributes(object attrs);
        IDropDownListExtension Events(Action<SmatEvent> smatEvent);

        IDropDownListExtension IsComboBox(bool isComboBox);

        MvcHtmlString ToHtmlString();
        void Render();
    }
}
