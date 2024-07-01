using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public interface IGridExtension
    {
        IGridExtension Name(string name);

        IGridExtension Enable(bool enable);

        IGridExtension Columns(Action<GridColumnExtension> columnBuilder);
        IGridExtension DataBound(string dataBound);
        IGridExtension DataSource(string dataSource);
        IGridExtension DataSource(IList dataSource);
        IGridExtension DataSource(Action<GridDataSource> dataSource);
        IGridExtension SendData(Action<GridSendData> sendDataBuilder);
        IGridExtension DetailTemplateId(string detailTemplateId);
        IGridExtension DetailTemplateInit(string detailTemplateInitFunc);
        IGridExtension RowTemplate(string rowTemplateFunc);
        IGridExtension TheadTemplateId(string theadTemplateId);
        IGridExtension CheckCellEditable(string checkCellEditable);
        IGridExtension Scrollable();
        IGridExtension Groupable();
        IGridExtension Sortable();
        IGridExtension Pageable();
        IGridExtension Selectable();


        IGridExtension Tooltip(string content);
        IGridExtension Tooltip(Action<SmatTooltip> smatTooltip);

        IGridExtension ValueChange(string valueChangeFunc);
        
        IGridExtension HtmlAttributes(object attrs);


        IGridExtension PrintPageSize(int printPageSize);
    }



    public interface IGridExtension<TModel> where TModel : class
    {
        IGridExtension<TModel> Name(string name);
        IGridExtension<TModel> Columns(Action<GridColumnExtension<TModel>> columnBuilder);
        IGridExtension<TModel> DataSource(Action<GridDataSource<TModel>> gridDataSource);
        IGridExtension<TModel> Groupable(bool groupable);
        IGridExtension<TModel> Scrollable(bool scrollable);
        IGridExtension<TModel> Sortable(bool sortable);
        IGridExtension<TModel> Pageable(bool pageable);

        IGridExtension<TModel> ValueChange(string valueChangeFunc);
    }
}
