using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Smat.Mvc.Extensions
{
    public class GridExtension : ComponentExtension, IGridExtension
    {
        private IList<IGridColumn> GridColumnList { get; set; }
        private bool? ScrollableAttribute { get; set; }
        private bool? GroupableAttribute { get; set; }
        private bool? SortableAttribute { get; set; }
        private bool? SelectableAttribute { get; set; }
        private bool? PageableAttribute { get; set; }
        private string DataBoundAttribute { get; set; }
        protected bool EnableAttribute { get; set; }

        private string DataSourceAttribute { get; set; }
        private IList DataSourceObjAttribute { get; set; }

        private GridDataSource GridDataSourceAttribute { get; set; }
        private GridSendData GridSendDataAttribute { get; set; }
        private string CheckCellEditableAttribute { get; set; }
        private string DetailTemplateIdAttribute { get; set; }
        private string DetailTemplateInitFuncAttribute { get; set; }
        private string RowTemplateAttribute { get; set; }

        private int PrintPageSizeAttribute { get; set; }


        private SmatTooltip SmatTooltip { get; set; }


        private string ValueChangeFuncAttribute { get; set; }

        /// <summary>
        /// TheadTemplateId
        /// </summary>
        private string TheadTemplateIdAttribute { get; set; }
        internal GridExtension(HtmlHelper helper,string id)
            :base(helper)
        {
            GridColumnList = new List<IGridColumn>();
            this.IdAttribute = id;
            this.EnableAttribute = true;
        }

        public IGridExtension Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IGridExtension Columns(Action<GridColumnExtension> columnBuilder)
        {
            GridColumnExtension builder = new GridColumnExtension(this);
            columnBuilder(builder);
            return this;
        }

        internal IGridColumn AddColumn(string filed)
        {
            GridColumn column = new GridColumn(filed);
            this.GridColumnList.Add(column);
            return column;
        }
        public IGridExtension DataBound(string dataBound)
        {
            this.DataBoundAttribute = dataBound;
            return this;
        }

        public IGridExtension DataSource(string dataSource)
        {
            this.DataSourceAttribute = dataSource;
            return this;
        }

        public IGridExtension DataSource(IList dataSource)
        {
            this.DataSourceObjAttribute = dataSource;
            return this;
        }

        public IGridExtension DataSource(Action<GridDataSource> dataSourceBuilder)
        {
            this.GridDataSourceAttribute = new GridDataSource();
            dataSourceBuilder(this.GridDataSourceAttribute);
            return this;
        }

        public IGridExtension SendData(Action<GridSendData> sendDataBuilder)
        {
            this.GridSendDataAttribute = new GridSendData();
            sendDataBuilder(this.GridSendDataAttribute);
            return this;
        }
        public IGridExtension RowTemplate(string rowTemplateFunc)
        {
            this.RowTemplateAttribute = rowTemplateFunc;
            return this;
        }

        public IGridExtension Tooltip(string content)
        {
            this.SmatTooltip = new SmatTooltip();
            this.SmatTooltip.Content(content);
            return this;
        }

        public IGridExtension Tooltip(Action<SmatTooltip> smatTooltip)
        {
            this.SmatTooltip = new SmatTooltip();
            smatTooltip(this.SmatTooltip);
            return this;
        }


        public IGridExtension Enable(bool enable)
        {
            this.EnableAttribute = enable;
            return this;
        }

        public IGridExtension TheadTemplateId(string theadTemplateId)
        {
            this.TheadTemplateIdAttribute = theadTemplateId;
            return this;
        }

        public IGridExtension DetailTemplateId(string detailTemplateId)
        {
            this.DetailTemplateIdAttribute = detailTemplateId;
            return this;
        }
        public IGridExtension DetailTemplateInit(string detailTemplateInitFunc)
        {
            this.DetailTemplateInitFuncAttribute = detailTemplateInitFunc;
            return this;
        }

        public IGridExtension CheckCellEditable(string checkCellEditable)
        {
            this.CheckCellEditableAttribute = checkCellEditable;
            return this;
        }

        public IGridExtension Scrollable()
        {
            this.ScrollableAttribute = true;
            return this;
        }

        public IGridExtension Groupable()
        {
            this.GroupableAttribute = true;
            return this;
        }

        public IGridExtension Sortable()
        {
            this.SortableAttribute = true;
            return this;
        }

        public IGridExtension Pageable()
        {
            this.PageableAttribute = true;
            return this;
        }

        public IGridExtension Selectable()
        {
            this.SelectableAttribute = true;
            return this;
        }

        public IGridExtension ValueChange(string valueChangeFunc)
        {
            this.ValueChangeFuncAttribute = valueChangeFunc;
            return this;
        }

        public IGridExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        public IGridExtension PrintPageSize(int printPageSize)
        {
            this.PrintPageSizeAttribute = printPageSize;
            return this;
        }


        protected override string CreateHtml()
        {
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) div.MergeAttribute("name", this.NameAttribute);
            div.MergeAttributes(attrs);
            return div.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IGridColumn gridColumn in this.GridColumnList)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }

                GridColumn column = (gridColumn as GridColumn);

                sb.AppendFormat("{{field:\"{0}\",title:\"{1}\"",
                    column.FieldAttribute,
                    column.TitleAttribute);

                if (column.WidthAttribute != null && column.WidthAttribute.Length > 0) sb.AppendFormat(",width:\"{0}\"", column.WidthAttribute);
                
                if (column.DataTypeAttribute != null && column.DataTypeAttribute.Length > 0) sb.AppendFormat(",dataType:\"{0}\"", column.DataTypeAttribute);

                if (column.CheckAllAttribute != null) sb.AppendFormat(",checkAll:{0}", column.CheckAllAttribute.Value ? "true" : "false");

                if (column.LockedAttribute != null) sb.AppendFormat(",locked:{0}", column.LockedAttribute.Value ? "true" : "false");

                if (column.SelectedDataNameAttribute != null && column.SelectedDataNameAttribute.Length > 0) sb.AppendFormat(",selectedDataName:\"{0}\"", column.SelectedDataNameAttribute);

                if (column.EditableAttribute != null) sb.AppendFormat(",editable:\"{0}\"", column.EditableAttribute);

                if (column.CodeKindAttribute != null) sb.AppendFormat(",codeKind:\"{0}\"", column.CodeKindAttribute);

                if (column.MaxAttribute != null) sb.AppendFormat(",max:{0}", column.MaxAttribute);

                if (column.MinAttribute != null) sb.AppendFormat(",min:{0}", column.MinAttribute);


                if (column.MaxDateAttribute != null) sb.AppendFormat(",maxDate:\"{0}\"", column.MaxDateAttribute);

                if (column.MinDateAttribute != null) sb.AppendFormat(",minDate:\"{0}\"", column.MinDateAttribute);


                if (column.MaxLengthAttribute != null) sb.AppendFormat(",maxlength:{0}", column.MaxLengthAttribute);


                if (column.SmatTooltip != null) sb.Append(column.SmatTooltip);


                if (column.EditorDataSourceAttribute != null) sb.AppendFormat(",editorDataSource:{0}", column.EditorDataSourceAttribute);

                if (column.ReferParamAttribute != null && column.ReferParamAttribute.Length > 0) sb.AppendFormat(",getReferParam:{0}", column.ReferParamAttribute);
                
                if (column.ReferSelectedAttribute != null && column.ReferSelectedAttribute.Length > 0) sb.AppendFormat(",referSelected:{0}", column.ReferSelectedAttribute);
                
                if (column.ValueFieldAttribute != null && column.ValueFieldAttribute.Length > 0) sb.AppendFormat(",valueField:\"{0}\"", column.ValueFieldAttribute);

                if (column.ReferKeyAttribute != null && column.ReferKeyAttribute.Length > 0) sb.AppendFormat(",referKey:\"{0}\"", column.ReferKeyAttribute);

                if (column.ReferValueFieldAttribute != null && column.ReferValueFieldAttribute.Length > 0) sb.AppendFormat(",referValueField:\"{0}\"", column.ReferValueFieldAttribute);

                if (column.ReferDisplayFieldAttribute != null && column.ReferDisplayFieldAttribute.Length > 0) sb.AppendFormat(",referDisplayField:\"{0}\"", column.ReferDisplayFieldAttribute);

                if (column.TemplateAttribute != null && column.TemplateAttribute.Length > 0) sb.AppendFormat(",template:\"{0}\"", column.TemplateAttribute);

                if (column.RadioValueAttribute != null && column.RadioValueAttribute.Length > 0) sb.AppendFormat(",radioValue:\"{0}\"", column.RadioValueAttribute);
                
                if (column.TemplateFuncAttribute != null && column.TemplateFuncAttribute.Length > 0) sb.AppendFormat(",template:{0}", column.TemplateFuncAttribute);

                if (column.IsRequiredAttribute != null) sb.AppendFormat(",isRequired:{0}", column.IsRequiredAttribute.Value ? "true" : "false");

                if (column.RowSpanFieldsAttribute != null && column.RowSpanFieldsAttribute.Length > 0) sb.AppendFormat(",rowSpanFields:\"{0}\"", column.RowSpanFieldsAttribute);

                if (column.FormatAttribute != null && column.FormatAttribute.Length > 0) sb.AppendFormat(",format:\"{0}\"", column.FormatAttribute);

                if (column.attrs != null && column.attrs.Count > 0) {
                    sb.Append(",attributes:{");
                    foreach (var item in column.attrs)
                    {
                          sb.Append("'"+ item.Key +"': '"+ item.Value +"'");
                    }
                    sb.Append("}");
                }

                if (column.headerAttrs != null && column.headerAttrs.Count > 0)
                {
                    sb.Append(",headerAttributes:{");
                    foreach (var item in column.headerAttrs)
                    {
                        sb.Append("'" + item.Key + "': '" + item.Value + "'");
                    }
                    sb.Append("}");
                }

                IList<IGridColumnAction> gridColumnActionList =(gridColumn as GridColumn).GridColumnActionList;
                if (gridColumnActionList != null && gridColumnActionList.Count > 0)
                {
                    StringBuilder actionsSb  =  new StringBuilder();
                    foreach (var gridColumnAction in gridColumnActionList)
                    {
                      GridColumnAction action =  (gridColumnAction as GridColumnAction);
                        if(actionsSb.Length == 0)
                        {
                            actionsSb.Append("{");
                        }else
                        {
                            actionsSb.Append(",{");
                        }
                        actionsSb.AppendFormat("text: \"{0}\"",action.TextAttribute);
                        if (!string.IsNullOrEmpty(action.ClickFuncAttribute)) actionsSb.AppendFormat(",click:{0}", action.ClickFuncAttribute);
                        if (!string.IsNullOrEmpty(action.ActionTypeAttribute)) actionsSb.AppendFormat(",actionType:\"{0}\"", action.ActionTypeAttribute);
                        if (!string.IsNullOrEmpty(action.ActionConfirmFuncAttribute)) actionsSb.AppendFormat(",actionConfirm:{0}", action.ActionConfirmFuncAttribute);
                        if (!string.IsNullOrEmpty(action.TemplateAttribute)) actionsSb.AppendFormat(",template:\"{0}\"", action.TemplateAttribute);
                        if (!string.IsNullOrEmpty(action.TemplateFuncAttribute)) actionsSb.AppendFormat(",template:{0}", action.TemplateFuncAttribute);
                        actionsSb.Append("}");
                    }
                    sb.AppendFormat(",actions: [{0}]",actionsSb.ToString());
                }
                sb.Append("}");
            }
            string columns = sb.ToString();

            sb.Clear();

            if (DataSourceObjAttribute != null && DataSourceObjAttribute.Count > 0)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                sb.Append(serializer.Serialize(this.DataSourceObjAttribute));
            }

            string dataSource = sb.ToString();

            sb.Clear();

            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatGrid({");
            sb.AppendFormat("columns: [{0}]", columns);
            if (!string.IsNullOrEmpty(DataSourceAttribute)) sb.AppendFormat(",dataSource:{0}", DataSourceAttribute);
            if (dataSource.Length > 0) sb.AppendFormat(",dataSource:{0}", dataSource);
            if (this.GridDataSourceAttribute != null) sb.AppendFormat(",dataSource:{0}", this.GridDataSourceAttribute);
            if (this.RowTemplateAttribute != null) sb.AppendFormat(",rowTemplate:{0}", this.RowTemplateAttribute);
            if (this.SmatTooltip != null) sb.Append(this.SmatTooltip);
            if (!string.IsNullOrEmpty(this.DetailTemplateIdAttribute)) sb.AppendFormat(",detailTemplateId:\"{0}\"", this.DetailTemplateIdAttribute);
            if (!string.IsNullOrEmpty(this.DetailTemplateInitFuncAttribute)) sb.AppendFormat(",detailTemplateInit:{0}", this.DetailTemplateInitFuncAttribute);
            if (!string.IsNullOrEmpty(this.TheadTemplateIdAttribute)) sb.AppendFormat(",theadTemplate:\"{0}\"", this.TheadTemplateIdAttribute);
            if (!string.IsNullOrEmpty(this.ValueChangeFuncAttribute)) sb.AppendFormat(",valueChange:{0}", this.ValueChangeFuncAttribute);
            if (!string.IsNullOrEmpty(this.DataBoundAttribute)) sb.AppendFormat(",dataBound:{0}", this.DataBoundAttribute);
            if (this.GridSendDataAttribute != null) sb.Append(",sendData:{" + this.GridSendDataAttribute + "}");
            if (this.CheckCellEditableAttribute != null) sb.AppendFormat(",checkCellEditable:{0}", this.CheckCellEditableAttribute);
            if (this.GroupableAttribute != null) sb.AppendFormat(",groupable: {0}", this.GroupableAttribute.Value);
            if (this.ScrollableAttribute != null) sb.AppendFormat(",scrollable: {0}", this.ScrollableAttribute.Value.ToString().ToLower());
            if (this.SortableAttribute != null) sb.AppendFormat(",sortable: {0}", this.SortableAttribute.Value.ToString().ToLower());
            if (this.SelectableAttribute != null) sb.AppendFormat(",selectable: {0}", this.SelectableAttribute.Value.ToString().ToLower());
            if (this.PrintPageSizeAttribute != 0) sb.AppendFormat(",printPageSize: {0}", this.PrintPageSizeAttribute.ToString().ToLower());
            if (this.PageableAttribute != null) sb.AppendFormat(",pageable: {0}", this.PageableAttribute.Value.ToString().ToLower());
            if (!this.EnableAttribute) sb.Append(",enable:false");
            sb.Append("});");
            return sb.ToString();
        }

    }

    public class GridExtension<TModel> : ComponentExtension, IGridExtension<TModel> where TModel : class
    {
        protected string TitleAttribute { get; set; }
        protected GridDataSource<TModel> GridDataSourceAttribute { get; set; }
        protected string GroupableAttribute { get; set; }
        protected string ScrollableAttribute { get; set; }
        protected string SortableAttribute { get; set; }
        protected string PageableAttribute { get; set; }
        protected string SelectableAttribute { get; set; }
        private string ValueChangeFuncAttribute { get; set; }
        protected IList<IGridColumn<TModel>> GridColumnList { get; set; }

        internal GridExtension(HtmlHelper helper)
            : base(helper)
        {
            this.GridColumnList = new List<IGridColumn<TModel>>();
        }

        public IGridExtension<TModel> Name(string name)
        {
            this.NameAttribute = name;
            return this;
        }

        public IGridExtension<TModel> Groupable(bool groupable)
        {
            this.GroupableAttribute = groupable ? "true" : "false";
            return this;
        }

        public IGridExtension<TModel> Scrollable(bool scrollable)
        {
            this.ScrollableAttribute = scrollable ? "true" : "false";
            return this;
        }

        public IGridExtension<TModel> Sortable(bool sortable)
        {
            this.SortableAttribute = sortable ? "true" : "false";
            return this;
        }

        public IGridExtension<TModel> Pageable(bool pageable)
        {
            this.PageableAttribute = pageable ? "true" : "false";
            return this;
        }

        public IGridExtension<TModel> Selectable(bool selectable)
        {
            this.SelectableAttribute = selectable ? "true" : "false";
            return this;
        }

        public IGridExtension<TModel> ValueChange(string valueChangeFunc)
        {
            this.ValueChangeFuncAttribute = valueChangeFunc;
            return this;
        }

        public IGridExtension<TModel> DataSource(Action<GridDataSource<TModel>> gridDataSource)
        {
            this.GridDataSourceAttribute = new GridDataSource<TModel>(this);
            gridDataSource(this.GridDataSourceAttribute);
            return this;
        }

        /// <summary>
        /// Add an lambda expression as a GridColumn.
        /// </summary>
        /// <typeparam name="TProperty">Model class property to be added as a column.</typeparam>
        /// <param name="expression">Lambda expression identifying a property to be rendered.</param>
        /// <returns>An instance of GridColumn.</returns>
        internal IGridColumn<TModel> AddColumn<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            GridColumn<TModel, TProperty> column = new GridColumn<TModel, TProperty>(expression);
            this.GridColumnList.Add(column);
            return column;
        }

        /// <summary>
        /// Create an instance of the ColumnBuilder to add columns to the table.
        /// </summary>
        /// <param name="columnBuilder">Delegate to create an instance of ColumnBuilder.</param>
        /// <returns>An instance of TableBuilder.</returns>
        public IGridExtension<TModel> Columns(Action<GridColumnExtension<TModel>> columnBuilder)
        {
            GridColumnExtension<TModel> builder = new GridColumnExtension<TModel>(this);
            columnBuilder(builder);
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("id", this.NameAttribute);

            return div.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IGridColumn<TModel> gridColumn in this.GridColumnList)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }
                sb.AppendFormat("{{field:\"{0}\",title:\"{1}\"",
                    (gridColumn as IGridColumnInternal<TModel>).FieldAttribute,
                    (gridColumn as IGridColumnInternal<TModel>).TitleAttribute);

                string widthAttribute = (gridColumn as IGridColumnInternal<TModel>).WidthAttribute;
                if (widthAttribute !=null && widthAttribute.Length > 0) sb.AppendFormat(",width:{0}",widthAttribute);

                sb.Append("}");
            }
            string columns = sb.ToString();

            sb.Clear();

            if (this.GridDataSourceAttribute.DataAttribute != null)
            {
                foreach (TModel model in this.GridDataSourceAttribute.DataAttribute)
                {    
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append("{");
                    for (int idx = 0; idx < this.GridColumnList.Count; idx++)
                    {
                        IGridColumn<TModel> gridColumn = this.GridColumnList[idx];
                        if (idx > 0)
                        {
                            sb.Append(",");
                        }
                        sb.AppendFormat("{0}:\"{1}\"",
                            (gridColumn as IGridColumnInternal<TModel>).FieldAttribute,
                            (gridColumn as IGridColumnInternal<TModel>).Evaluate(model));
                    }
                    sb.Append("}");
                }
            }
            string data = sb.ToString();

            sb.Clear();
            sb.Append("jQuery(\"#");
            sb.Append(this.NameAttribute);
            sb.Append("\").smatGrid({");
            sb.AppendFormat("columns: [{0}]", columns);
            if (data.Length > 0)
            {
                sb.AppendFormat(",dataSource: {{data: [{0}]{1}}}", data,
                    this.GridDataSourceAttribute.PageSizeAttribute.Length > 0
                        ? string.Format(",pageSize: {0}", this.GridDataSourceAttribute.PageSizeAttribute)
                        : string.Empty);
            }
            if (this.GroupableAttribute.Length > 0) sb.AppendFormat(",groupable: {0}", this.GroupableAttribute);
            if (this.ScrollableAttribute.Length > 0) sb.AppendFormat(",scrollable: {0}", this.ScrollableAttribute);
            if (!string.IsNullOrEmpty(this.ValueChangeFuncAttribute)) sb.AppendFormat(",valueChange:{0}", this.ValueChangeFuncAttribute);
            if (this.SortableAttribute.Length > 0) sb.AppendFormat(",sortable: {0}", this.SortableAttribute);
            if (this.PageableAttribute.Length > 0) sb.AppendFormat(",pageable: {0}", this.PageableAttribute);
            sb.Append("});");

            return sb.ToString();
        }
    }

}
