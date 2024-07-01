
using System;
namespace Smat.Mvc.Extensions
{
    public interface IGridColumn
    {
        IGridColumn Title(string title);
        IGridColumn Width(string width);
        IGridColumn DataType(string dataType);
        IGridColumn CheckAll(bool checkAll);
        IGridColumn SelectedDataName(string selectedDataName);
        IGridColumn Editable(bool editable);
        IGridColumn Editable(string editable);
        IGridColumn CodeKind(string codeKind);
        IGridColumn Max(int max);
        IGridColumn Min(int min);
        IGridColumn MaxDate(string maxDate);
        IGridColumn MinDate(string minDate);
        IGridColumn MaxLength(int maxLength);
        IGridColumn EditorDataSource(string editorDataSource);
        IGridColumn GetReferParam(string referParam);
        IGridColumn ReferSelected(string referSelected);
        IGridColumn ValueField(string valueField);
        IGridColumn ReferKey(string referKey);
        IGridColumn ReferValueField(string referValueField);
        IGridColumn ReferDisplayField(string referDisplayField);
        IGridColumn Template(string template);
        IGridColumn TemplateBound(string templateFunc);
        IGridColumn HtmlAttributes(object attrs);
        IGridColumn HeaderHtmlAttributes(object headerAttrs);
        IGridColumn RowSpanFields(string rowSpanFields);

        IGridColumn Format(string format);

        IGridColumn RadioValue(string radioValue);

        IGridColumn Locked();

        IGridColumn Actions(Action<GridColumnActionExtension> gridColumnAction);
        IGridColumn IsRequired(bool isRequired);


        IGridColumn Tooltip(string content);
        IGridColumn Tooltip(Action<SmatTooltip> smatTooltip);
    }
    public interface IGridColumn<TModel> where TModel : class
    {
        IGridColumn<TModel> Title(string title);
        IGridColumn<TModel> Width(int width);
    }
}
