using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public class GridColumn : IGridColumn
    {
        public string FieldAttribute { get; internal set; }
        public string TitleAttribute { get; internal set; }
        public string DataTypeAttribute { get; internal set; }
        public bool? CheckAllAttribute { get;internal set; }

        public bool? LockedAttribute { get; set; }

        public string SelectedDataNameAttribute { get; internal set; }
        public string EditableAttribute { get; internal set; }
        public string CodeKindAttribute { get; internal set; }
        public int? MaxAttribute { get; internal set; }
        public int? MinAttribute { get;internal set; }

        public string MaxDateAttribute { get; internal set; }
        public string MinDateAttribute { get; internal set; }
        public int? MaxLengthAttribute { get; internal set; }
        public string EditorDataSourceAttribute { get; internal set; }
        public string ReferParamAttribute { get; internal set; }
        public string ReferSelectedAttribute { get; internal set; }
        public string ValueFieldAttribute { get; internal set; }
        public string ReferKeyAttribute { get; internal set; }
        public string ReferValueFieldAttribute { get; internal set; }
        public string ReferDisplayFieldAttribute { get; internal set; }
        public string WidthAttribute { get; internal set; }
        public string TemplateAttribute { get; internal set; }
        public string TemplateFuncAttribute { get; internal set; }
        public bool? IsRequiredAttribute { get; internal set; }

        public string RowSpanFieldsAttribute { get; internal set; }

        public string FormatAttribute { get; internal set; }

        public string RadioValueAttribute { get; internal set; }

        public IDictionary<string, object> attrs { get; internal set; }
        public IDictionary<string, object> headerAttrs { get; internal set; }

        public SmatTooltip SmatTooltip { get; set; }

        public IList<IGridColumnAction> GridColumnActionList { get; set; }
        internal GridColumn(string filed)
        {
            this.FieldAttribute = filed;
            GridColumnActionList = new List<IGridColumnAction>();
        }

        public IGridColumn Title(string title)
        {
            this.TitleAttribute = title;
            return this;
        }

        public IGridColumn Width(string width)
        {
            this.WidthAttribute = width;
            return this;
        }

        public IGridColumn RowSpanFields(string rowSpanFields)
        {
            this.RowSpanFieldsAttribute = rowSpanFields;
            return this;
        }

        public IGridColumn Format(string format)
        {
            this.FormatAttribute = format;
            return this;
        }

        public IGridColumn RadioValue(string radioValue)
        {
            this.RadioValueAttribute = radioValue;
            return this;
        }

        public IGridColumn DataType(string dataType)
        {
            this.DataTypeAttribute = dataType;
            return this;
        }

        public IGridColumn CheckAll(bool checkAll)
        {
            this.CheckAllAttribute = checkAll;
            return this;
        }

        public IGridColumn Locked()
        {
            this.LockedAttribute = true;
            return this;
        }
        

        public IGridColumn SelectedDataName(string selectedDataName)
        {
            this.SelectedDataNameAttribute = selectedDataName;
            return this;
        }

        public IGridColumn Editable(bool editable)
        {
            this.EditableAttribute = editable ? "true" : "false";
            return this;
        }

        public IGridColumn Editable(string editable)
        {
            this.EditableAttribute = editable;
            return this;
        }

        public IGridColumn CodeKind(string codeKind)
        {
            this.CodeKindAttribute = codeKind;
            return this;
        }

        public IGridColumn Max(int max)
        {
            this.MaxAttribute = max;
            return this;
        }
        public IGridColumn Min(int min) 
        {
            this.MinAttribute = min;
            return this;
        }

        public IGridColumn MaxDate(string maxDate)
        {
            this.MaxDateAttribute = maxDate;
            return this;
        }
        public IGridColumn MinDate(string minDate)
        {
            this.MinDateAttribute = minDate;
            return this;
        }

        public IGridColumn MaxLength(int maxLength)
        {
            this.MaxLengthAttribute = maxLength;
            return this;
        }

        public IGridColumn EditorDataSource(string editorDataSource)
        {
            this.EditorDataSourceAttribute = editorDataSource;
            return this;
        }

        public IGridColumn GetReferParam(string referParam)
        {
            this.ReferParamAttribute = referParam;
            return this;
        }

        public IGridColumn ReferSelected(string referSelected) 
        {
            this.ReferSelectedAttribute = referSelected;
            return this;
        }
        public IGridColumn ValueField(string valueField)
        {
            this.ValueFieldAttribute = valueField;
            return this;
        }
        public IGridColumn ReferKey(string referKey)
        {
            this.ReferKeyAttribute = referKey;
            return this;
        }
        public IGridColumn ReferValueField(string referValueField)
        {
            this.ReferValueFieldAttribute = referValueField;
            return this;
        }
        public IGridColumn ReferDisplayField(string referDisplayField)
        {
            this.ReferDisplayFieldAttribute = referDisplayField;
            return this;
        }

        public IGridColumn Template(string template)
        {
            this.TemplateAttribute = template;
            return this;
        }

        public IGridColumn TemplateBound(string templateFunc)
        {
            this.TemplateFuncAttribute = templateFunc;
            return this;
        }
        public IGridColumn HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        public IGridColumn HeaderHtmlAttributes(object headerAttrs)
        {
            this.headerAttrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(headerAttrs));
            return this;
        }

        public IGridColumn Actions(Action<GridColumnActionExtension> gridColumnAction)
        {
            GridColumnActionExtension builder = new GridColumnActionExtension(this);
            gridColumnAction(builder);
            return this;
        }

        internal IGridColumnAction AddAction(string text)
        {
            GridColumnAction column = new GridColumnAction(text);
            this.GridColumnActionList.Add(column);
            return column;
        }

        public IGridColumn IsRequired(bool isRequired)
        {
            this.IsRequiredAttribute = isRequired;
            return this;
        }

        public IGridColumn Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public IGridColumn Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }

    }
    public class GridColumn<TModel, TProperty> : IGridColumn<TModel>, IGridColumnInternal<TModel> where TModel : class
    {
        /// <summary>
        /// Compiled lambda expression to get the property value from a model object.
        /// </summary>
        public Func<TModel, TProperty> CompiledExpression { get; set; }

        public string FieldAttribute { get; internal set; }
        public string TitleAttribute { get; internal set; }
        public string RowSpanFieldsAttribute { get; internal set; }
        public string FormatAttribute { get; internal set; }

        public string RadioValueAttribute { get; internal set; }

        public string WidthAttribute { get; internal set; }

        internal GridColumn()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="expression">Lambda expression identifying a property to be rendered.</param>
        public GridColumn(Expression<Func<TModel, TProperty>> expression)
        {
            this.FieldAttribute = (expression.Body as MemberExpression).Member.Name;
            this.TitleAttribute = Regex.Replace(this.FieldAttribute, "([a-z])([A-Z])", "$1 $2");
            this.CompiledExpression = expression.Compile();
        }

        public IGridColumn<TModel> Title(string title)
        {
            this.TitleAttribute = title;
            return this;
        }

        public IGridColumn<TModel> RowSpanFields(string rowSpanFields)
        {
            this.RowSpanFieldsAttribute = rowSpanFields;
            return this;
        }

        public IGridColumn<TModel> Format(string format)
        {
            this.FormatAttribute = format;
            return this;
        }

        public IGridColumn<TModel> RadioValue(string radioValue)
        {
            this.RadioValueAttribute = radioValue;
            return this;
        }

        public IGridColumn<TModel> Width(int width)
        {
            this.WidthAttribute = width.ToString();
            return this;
        }

        /// <summary>
        /// Get the property value from a model object.
        /// </summary>
        /// <param name="model">Model to get the property value from.</param>
        /// <returns>Property value from the model.</returns>
        public string Evaluate(TModel model)
        {
            var result = this.CompiledExpression(model);
            return result == null ? string.Empty : result.ToString();
        }
    }
}
